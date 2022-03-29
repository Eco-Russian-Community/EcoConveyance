using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Mods.EcoConveyance.Animation;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Eco.Gameplay.Components;
using Eco.World.Blocks;

namespace Eco.Mods.EcoConveyance.Components
{
	using World = Eco.World.World;

	[Serialized]
	[RequireComponent(typeof(ChunkSubscriberComponent))]
	internal abstract class BaseConveyorComponent : WorldObjectComponent, ITickOnDemand, IChunkSubscriber
	{
		[Serialized] public Direction[] OutputDirection { get; set; }
		[Serialized] public Direction[] InputDirection { get; set; }
		public ThreadSafeDictionary<Direction, BaseConveyorObject> DestinationConveyor { get; } = new ThreadSafeDictionary<Direction, BaseConveyorObject>();

		public float Speed
		{
			get { return 1000f / this.ConveyorSpeed; }
			set { this.ConveyorSpeed = (int)Math.Round(1000f / value); }
		}
		public int ConveyorSpeed { get; private set; } = 1000;

		public static bool IsShutdown { get; internal set; } = false;

		[Serialized] protected volatile CrateData CrateData;

		private readonly Object _operationLock = new Object();
		public readonly ThreadSafeAction CrateStuck = new ThreadSafeAction();
		public readonly ThreadSafeAction<BaseConveyorComponent> MovedOut = new ThreadSafeAction<BaseConveyorComponent>();

		protected abstract void CrateArrived();
		protected abstract void TryMoveOut(Direction direction);
		protected abstract void TryMoveOut();

		#region IChunkSubscriber
		public float UpdateFrequencySec => 5f;
		public float MaxQueuedChunkUpdateTime => 150f;
		public double QueuedChunkUpdateTime { get; set; }
		public double LastChunkUpdateTime { get; set; }

		public void ChunksChanged()
		{
			try
			{
				this.UpdateDestination();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		public IEnumerable<Vector3i> RelevantChunkPositions()
		{
			List<Vector3i> result = new List<Vector3i>();
			if (this.OutputDirection != null)
			{
				foreach (Direction dir in this.OutputDirection)
				{
					if (Direction.Unknown.Equals(dir) || Direction.None.Equals(dir)) { continue; }
					IEnumerable<Vector3i> positions = Vector3iUtils.OccupancyWorldPosition(this.Parent).Select(pos => World.GetWrappedWorldPosition(pos + dir.ToVec()));
					result.AddRange(positions.Select(pos => World.ToChunkPosition(pos)));
				}
			}
			return result.Distinct();
		}
		#endregion

		public override void Initialize()
		{
			base.Initialize();
			try
			{
				if (this.OutputDirection == null) { this.OutputDirection = new Direction[] { Direction.Unknown }; }
				if (this.InputDirection == null) { this.InputDirection = new Direction[] { Direction.Unknown }; }
				this.CrateStuck.Add(this.OnCrateStuck);
				this.UpdateDestination();
				if (this.CheckCrateData())
				{
					this.CrateData.Crate.OnDestroy.Add(this.OnCrateDestroy);
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			DebuggingUtils.LogInfoLine($"BaseConveyorComponent: [{this.Parent}] Initialize(\n\tInputDirection: {string.Join(',', this.InputDirection)}\n\tOutputDirection: {string.Join(',', this.OutputDirection)}\n\tDestinationConveyor: {string.Join(',', this.DestinationConveyor)}\n\tCrateData: {this.CrateData}\n)");
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
				if (this.CheckCrateData()) { this.TryMoveOut(); }
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		public void UpdateDestination()
		{
			DebuggingUtils.LogErrorLine($"BaseConveyorComponent: [{this.Parent}] UpdateDestination()");
			try
			{
				if (this.OutputDirection == null) { return; }

				List<Vector3i> positions = Vector3iUtils.OccupancyWorldPosition(this.Parent);

				foreach (Direction dir in this.OutputDirection)
				{
					if (Direction.Unknown.Equals(dir) || Direction.None.Equals(dir)) { continue; }
					foreach (Vector3i position in positions)
					{
						Vector3i neededPosition = World.GetWrappedWorldPosition(position + dir.ToVec());
						Block block = World.GetBlock(neededPosition);
						if(block != null && block is WorldObjectBlock objectBlock)
						{
							WorldObject worldObject = objectBlock.WorldObjectHandle.Object;
							if (worldObject is BaseConveyorObject conveyorObj)
							{
								if (this.Parent.Equals(conveyorObj)) { continue; }
								if (!this.DestinationConveyor.ContainsKey(dir))
								{ // Destination for direction not registered
									this.DestinationConveyor.Add(dir, conveyorObj);
									conveyorObj.OnDestroy.Add(this.OnDestinationDestroy);
									DebuggingUtils.LogInfoLine($"BaseConveyorComponent: [{this.Parent}] UpdateDestination Add[{dir}={conveyorObj}]");
								}
								else
								{ // Destination for direction is registered
									bool result = this.DestinationConveyor.TryGetValue(dir, out BaseConveyorObject exDestinationObj);
									if(!result || !conveyorObj.Equals(exDestinationObj))
									{
										this.DestinationConveyor.Remove(dir);
										this.DestinationConveyor.Add(dir, conveyorObj);
										conveyorObj.OnDestroy.Add(this.OnDestinationDestroy);
										DebuggingUtils.LogInfoLine($"BaseConveyorComponent: [{this.Parent}] UpdateDestination Update[{dir}={conveyorObj}]");
									}
								}
							}
						}
					}
				}

				this.UpdateVisual();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected void UpdateVisual()
		{
			//Detection of placing in row
			Direction thisDirection = DirectionExtensions.FacingDir(this.Parent.Rotation.Forward);
			foreach(KeyValuePair<Direction, BaseConveyorObject> dst in this.DestinationConveyor)
			{
				Direction dstDirection = DirectionExtensions.FacingDir(dst.Value.Rotation.Forward);
				this.Parent.SetAnimatedState("StraightConnection", thisDirection.Equals(dstDirection));
			}
		}

		protected void TryMoveOut(Direction direction, BaseConveyorComponent conveyor)
		{
			//DebuggingUtils.LogInfoLine($"BaseConveyorComponent: TryMoveOut({direction})");
			if (EcoConveyance.IsShutdown) { DebuggingUtils.LogWarningLine("BaseConveyorComponent: Prepare to shutdown, stop operating"); return; }
			try
			{
				if (conveyor.Parent.Enabled && //!conveyor.Parent.Operating &&
					conveyor.CanReceiveFrom(this))
				{
					if(conveyor.ReceiveCrate(this.CrateData, this))
					{
						conveyor.MovedOut.Add(this.OnMovedOut);
						this.Parent.TriggerAnimatedEvent($"MoveOut");
						this.CrateData.Crate.SetAnimatedState("Speed", this.Speed);
						conveyor.Animate(new Animation.Animation($"Move{direction}", this.ConveyorSpeed));
						this.CrateData.Crate.OnDestroy.Remove(this.OnCrateDestroy);
						this.CrateData = null;

						DebuggingUtils.LogInfoLine($"BaseConveyorComponent: MoveOut with speed [{this.Speed}][{this.ConveyorSpeed}]");
					}
					else
					{
						DebuggingUtils.LogWarningLine($"BaseConveyorComponent: TryMoveOut({direction}) but DestinationConveyor({conveyor}) is busy!");
						this.CrateStuck.Invoke();
					}
				}
				else
				{
					DebuggingUtils.LogWarningLine($"BaseConveyorComponent: TryMoveOut({direction}) but DestinationConveyor({conveyor}) is not operating! [{conveyor.Parent.Enabled}][{conveyor.CanReceiveFrom(this)}]");
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
						if (crateData.Crate == null) { return false; }
						this.Parent.TriggerAnimatedEvent($"ReceiveCrate");
						this.CrateData = crateData.ChangeSource(sourceConveyor);
						this.CrateData.Crate.OnDestroy.Add(this.OnCrateDestroy);
						return true;
					}
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			return false;
		}

		/// <summary>Called from another conveyor to animate crate and signal this conveyor when animation is done.</summary>
		public bool Animate(Animation.Animation animation)
		{
			return this.CrateData.Crate.AnimationController.PlayAnimationSynced(animation, this.Moved);
		}

		private void Moved()
		{
			//DebuggingUtils.LogInfoLine("ConveyorComponent: Moved()");
			try
			{
				if (this.CheckCrateData())
				{
					this.CrateData.Crate.Position = this.Parent.Position;
					this.MovedOut.Invoke(this);
					this.CrateArrived();
				}
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
			Direction sourceDir = DirectionUtils.GetDeltaDirection(conveyor.Parent.Position3i, this.Parent.Position3i);
			return this.InputDirection.Contains(sourceDir);
		}

		protected bool CheckCrateData()
		{
			if (this.CrateData != null)
			{
				if (this.CrateData.Crate != null)
				{ return true; }
				else
				{
					this.OnCrateDestroy();
					return false;
				}
			}
			return false;
		}

		protected void DestroyCrate()
		{
			try
			{
				if (this.CheckCrateData())
				{
					DebuggingUtils.LogInfoLine($"DestroyCrate: {this.CrateData.Crate}");
					this.CrateData.Crate.Destroy();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		private void OnCrateStuck()
		{
			//DebuggingUtils.LogInfoLine($"BaseConveyorComponent: OnCrateStuck()");
			if (this.CheckCrateData())
			{
				ServiceHolder<IWorldObjectManager>.Obj.AddToTick(this);
			}
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
