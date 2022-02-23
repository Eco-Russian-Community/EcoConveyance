using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.World.Blocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Components
{
	using World = Eco.World.World;

	internal abstract class BaseConveyorComponent : WorldObjectComponent, IOperatingWorldObjectComponent, ITickOnDemand
	{
		[Serialized] public Direction[] OutputDirection { get; set; }
		public Dictionary<Direction, BaseConveyorObject> DestinationConveyor { get; } = new Dictionary<Direction,BaseConveyorObject>();
		public virtual bool CanReceive { get; } = true;

		public bool Operating => this._op;
		protected bool _op = true;

		[Serialized] protected CrateData CrateData;

		private readonly Object _operationLock = new Object();
		public readonly ThreadSafeAction CrateStuck = new ThreadSafeAction();

		protected abstract void CrateArrived();
		protected abstract void TryMoveOut(Direction direction);
		protected abstract void TryMoveOut();

		public override void Initialize()
		{
			base.Initialize();
			try
			{
				if(this.OutputDirection == null) { this.OutputDirection = new Direction[] {Direction.Unknown}; }
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
				
		//	}
		//	catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		//}

		public void TickOnDemand()
		{
			try
			{
				if (this.CrateData != null){ this.TryMoveOut(); }
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		public void UpdateDestination()
		{
			try
			{
				this.DestinationConveyor.Clear();
				if(this.OutputDirection == null) { return; }

				foreach(Direction dir in this.OutputDirection)
				{
					if (Direction.Unknown.Equals(dir) || Direction.None.Equals(dir)) { continue; }
					Vector3i neededPosition = this.Parent.Position.Round + dir.ToVec();
					foreach (WorldObject worldObject in ServiceHolder<IWorldObjectManager>.Obj.GetObjectsWithin(this.Parent.Position, 1f))
					{
						if (worldObject.Equals(this.Parent)) { continue; }
						if (!worldObject.Position3i.Equals(neededPosition)) { continue; }
						if (worldObject is BaseConveyorObject conveyor)
						{
							this.DestinationConveyor.Add(dir, conveyor);
							conveyor.OnDestroy.Add(this.OnDestinationDestroy);
							DebuggingUtils.LogInfoLine($"BaseConveyorComponent: UpdateDestination Add[{dir}={conveyor}]");
							//IEnumerable<ConveyorComponent> components = obj.GetComponents<ConveyorComponent>();
							//foreach (ConveyorComponent component in components)
							//{

							//}
						}
					}
				}

				//Block block = World.GetBlock(World.GetWrappedWorldPosition(this.Parent.Position.Round + this.OutputDirection.ToVec()));
				//if (block == null || block is not WorldObjectBlock objBlock) { return; }
				//WorldObject obj = objBlock.WorldObjectHandle.Object;
				//if (obj is ConveyorWorldObject conveyor)
				//{
				//	this.DestinationConveyor = conveyor;
				//	this.DestinationConveyor.OnDestroy.Add(this.OnDestinationDestroy);
				//	Log.WriteWarningLineLocStr($"BaseConveyorComponent: UpdateDestination [{this.DestinationConveyor}]");
				//	//IEnumerable<ConveyorComponent> components = obj.GetComponents<ConveyorComponent>();
				//	//foreach (ConveyorComponent component in components)
				//	//{

				//	//}
				//}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		//public void UpdateNearby()
		//{
		//	foreach (DirectionAxis dir in Enum.GetValues(typeof(DirectionAxis)))
		//	{
		//		Block block = World.GetBlock(World.GetWrappedWorldPosition(this.Parent.Position.Round + dir.ToVec()));
		//		if (block == null || block is not WorldObjectBlock objBlock) { return; }
		//		WorldObject obj = objBlock.WorldObjectHandle.Object;
		//		if (obj is not ConveyorObject conveyor) { return; }


		//	}
		//}

		protected void TryMoveOut(Direction direction, BaseConveyorComponent conveyor)
		{
			DebuggingUtils.LogInfoLine($"BaseConveyorComponent: TryMoveOut{direction}");
			try
			{
				if (conveyor.CanReceive && conveyor.Operating && conveyor.ReceiveCrate(this.CrateData.Crate, this))
				{
					this.CrateData.Crate.TriggerAnimatedEvent($"Move{direction}");
					this.CrateData.Crate.OnDestroy.Remove(this.OnCrateDestroy);
					this.CrateData = null;
				}
				else
				{
					DebuggingUtils.LogWarningLine($"BaseConveyorComponent: TryMoveOut{direction} but DestinationConveyor({conveyor}) is not operating or busy!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		/// <summary>Called from another conveyor when it try to move crate to this conveyor.</summary>
		public bool ReceiveCrate(ConveyorCrateObject crate, BaseConveyorComponent sourceConveyor)
		{
			try
			{
				lock(this._operationLock)
				{
					if (this.CrateData == null)
					{
						this.CrateData = new CrateData(crate, sourceConveyor.Parent);
						crate.OnDestroy.Add(this.OnCrateDestroy);
						Timer timer = new Timer(new TimerCallback(this.Moved));
						return timer.Change(1000, Timeout.Infinite);
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
				this.CrateData.Crate.Position = this.Parent.Position;
				this.CrateArrived();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected void DestroyCrate()
		{
			try
			{
				if (this.CrateData != null && this.CrateData.Crate != null)
				{
					DebuggingUtils.LogInfoLine($"DestroyCrate: {this.CrateData.Crate}");
					this.CrateData.Crate.Destroy();
					this.CrateData = null;
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
			if(obj != null && this.DestinationConveyor.ContainsValue(obj))
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

				foreach(BaseConveyorObject conveyor in this.DestinationConveyor.Values)
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
