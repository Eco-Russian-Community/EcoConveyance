using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Eco.Mods.EcoConveyance.Components
{
	using World = Eco.World.World;

	[Serialized]
	internal abstract class BaseConveyorComponent : WorldObjectComponent, IOperatingWorldObjectComponent, ITickOnDemand
	{
		[Serialized] public Direction[] OutputDirection { get; set; }
		[Serialized] public Direction[] InputDirection { get; set; }
		public Dictionary<Direction, BaseConveyorObject> DestinationConveyor { get; } = new Dictionary<Direction, BaseConveyorObject>();
		public virtual bool CanReceive { get; } = true;
		public float Speed
		{
			get { return 1000f / this.ConveyorSpeed; }
			set { this.ConveyorSpeed = (int)Math.Round(1000f / value); }
		}
		public int ConveyorSpeed { get; private set; } = 1000;

		public bool Operating => this._op;
		protected bool _op = false;

		public static bool IsShutdown { get; internal set; } = false;

		[Serialized] protected volatile CrateData CrateData;

		private readonly Object _operationLock = new Object();
		public readonly ThreadSafeAction CrateStuck = new ThreadSafeAction();
		public readonly ThreadSafeAction<BaseConveyorComponent> MovedOut = new ThreadSafeAction<BaseConveyorComponent>();

		protected abstract void CrateArrived();
		protected abstract void TryMoveOut(Direction direction);
		protected abstract void TryMoveOut();

		public override void Initialize()
		{
			base.Initialize();
			try
			{
				if (this.OutputDirection == null) { this.OutputDirection = new Direction[] { Direction.Unknown }; }
				if (this.InputDirection == null) { this.InputDirection = new Direction[] { Direction.Unknown }; }
				this.CrateStuck.Add(this.OnCrateStuck);
				if (this.CrateData != null)
				{
					this.CrateData.Crate.OnDestroy.Add(this.OnCrateDestroy);
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			DebuggingUtils.LogInfoLine($"BaseConveyorComponent: Initialize(\n\tdirection: {string.Join(',', this.OutputDirection)}\n\tdestination: {string.Join(',', this.DestinationConveyor)}\n\tconveyorCrate: {this.CrateData})");
		}

		//public override void Tick()
		//{
		//	base.Tick();
		//	try
		//	{
		//		Log.WriteWarningLineLocStr("BaseConveyorComponent: Tick()");
		//	}
		//	catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		//}

		public void TickOnDemand()
		{
			try
			{
				if (this.CrateData != null) { this.TryMoveOut(); }
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		public void UpdateDestination()
		{
			try
			{
				this.DestinationConveyor.Clear();
				if (this.OutputDirection == null) { return; }
				Direction thisDirection = DirectionExtensions.FacingDir(this.Parent.Rotation.Forward);

				foreach (Direction dir in this.OutputDirection)
				{
					if (Direction.Unknown.Equals(dir) || Direction.None.Equals(dir)) { continue; }
					Vector3i neededPosition = World.GetWrappedWorldPosition(this.Parent.Position.Round + dir.ToVec());
					foreach (WorldObject worldObject in ServiceHolder<IWorldObjectManager>.Obj.GetObjectsWithin(this.Parent.Position, 1f))
					{
						if (worldObject.Equals(this.Parent)) { continue; }
						if (!worldObject.Position3i.Equals(neededPosition)) { continue; }
						if (worldObject is BaseConveyorObject conveyor)
						{
							this.DestinationConveyor.Add(dir, conveyor);
							conveyor.OnDestroy.Add(this.OnDestinationDestroy);
							//Detection of placing in row
							Direction dstDirection = DirectionExtensions.FacingDir(conveyor.Rotation.Forward);
							this.Parent.SetAnimatedState("StraightConnection", thisDirection.Equals(dstDirection));
							//
							DebuggingUtils.LogInfoLine($"BaseConveyorComponent: UpdateDestination Add[{dir}={conveyor}]");
							//IEnumerable<ConveyorComponent> components = obj.GetComponents<ConveyorComponent>();
							//foreach (ConveyorComponent component in components)
							//{

							//}
						}
					}
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected void TryMoveOut(Direction direction, BaseConveyorComponent conveyor)
		{
			DebuggingUtils.LogInfoLine($"BaseConveyorComponent: TryMoveOut({direction})");
			if (EcoConveyance.IsShutdown) { DebuggingUtils.LogWarningLine("BaseConveyorComponent: Prepare to shutdown, stop operating"); return; }
			try
			{
				if (conveyor.CanReceive &&
					conveyor.Parent.Enabled && !conveyor.Parent.Operating &&
					conveyor.CanReceiveFrom(this) &&
					conveyor.ReceiveCrate(this.CrateData, this))
				{
					conveyor.MovedOut.Add(this.OnMovedOut);
					this.Parent.TriggerAnimatedEvent($"MoveOut");
					this.CrateData.Crate.SetAnimatedState("Speed", this.Speed);
					this.CrateData.Crate.TriggerAnimatedEvent($"Move{direction}");
					this.CrateData.Crate.OnDestroy.Remove(this.OnCrateDestroy);
					this.CrateData = null;

					DebuggingUtils.LogErrorLine($"BaseConveyorComponent: MoveOut with speed [{this.Speed}][{this.ConveyorSpeed}]");
				}
				else
				{
					DebuggingUtils.LogWarningLine($"BaseConveyorComponent: TryMoveOut({direction}) but DestinationConveyor({conveyor}) is not operating or busy!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		/// <summary>Called from another conveyor when it try to move crate to this conveyor.</summary>
		public bool ReceiveCrate(CrateData crateData, BaseConveyorComponent sourceConveyor)
		{
			try
			{
				lock (this._operationLock)
				{
					if (this.CrateData == null && !EcoConveyance.IsShutdown)
					{
						this.Parent.TriggerAnimatedEvent($"ReceiveCrate");
						this.CrateData = crateData.ChangeSource(sourceConveyor);
						this.CrateData.Crate.OnDestroy.Add(this.OnCrateDestroy);
						this.CrateData.Crate.Position = this.Parent.Position;
						Timer timer = new Timer(new TimerCallback(this.Moved));
						return timer.Change(sourceConveyor.ConveyorSpeed, Timeout.Infinite);
					}
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			return false;
		}

		private void Moved(object timer)
		{ //TODO: System.NullReferenceException: Object reference not set to an instance of an object. Need to find where
			DebuggingUtils.LogInfoLine("ConveyorComponent: Moved()");
			try
			{
				Timer t = (Timer)timer;
				t.Dispose();
				//this.CrateData.Crate.Position = this.Parent.Position;
				this.MovedOut.Invoke(this);
				this.CrateArrived();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected virtual void OnMovedOut(BaseConveyorComponent conveyor)
		{
			conveyor.MovedOut.Remove(this.OnMovedOut);
		}

		public bool CanReceiveFrom(BaseConveyorComponent conveyor)
		{
			if (this.InputDirection.Contains(Direction.None)) { return false; }
			if (this.InputDirection.Contains(Direction.Unknown)) { return true; }
			Direction sourceDir = WorldPosition3i.GetDelta(this.Parent.Position3i, conveyor.Parent.Position3i).ToDir();
			return this.InputDirection.Contains(sourceDir);
		}

		protected void DestroyCrate()
		{
			try
			{
				if (this.CrateData != null && this.CrateData.Crate != null)
				{
					DebuggingUtils.LogInfoLine($"DestroyCrate: {this.CrateData.Crate}");
					this.CrateData.Crate.Destroy();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		private void OnCrateStuck()
		{
			DebuggingUtils.LogInfoLine($"BaseConveyorComponent: OnCrateStuck()");
			ServiceHolder<IWorldObjectManager>.Obj.AddToTick(this);
		}

		protected void OnDestinationDestroy(BaseConveyorObject obj)
		{
			if (obj != null && this.DestinationConveyor.ContainsValue(obj))
			{
				Direction dir = this.DestinationConveyor.FirstOrDefault(x => x.Value.Equals(obj)).Key;
				this.DestinationConveyor.Remove(dir);
			}
		}
		protected void OnCrateDestroy() => this.CrateData = null;

		public override void Destroy()
		{
			try
			{
				this.DestroyCrate();
				this.CrateStuck.Remove(this.OnCrateStuck);

				foreach (BaseConveyorObject conveyor in this.DestinationConveyor.Values)
				{
					conveyor.OnDestroy.Remove(this.OnDestinationDestroy);
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			base.Destroy();
		}

		//public ConveyorWorldObject GetObject() => this.GetObject<ConveyorWorldObject>();
		//public T GetObject<T>() where T : ConveyorWorldObject => this.Parent as T;
	}
}
