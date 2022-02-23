using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Linq;

namespace Eco.Mods.EcoConveyance.Components
{
	[Serialized]
	internal class ConveyorVerticalComponent : BaseConveyorComponent
	{
		public override void Initialize()
		{
			base.Initialize();
			try
			{
				this.UpdateDestination();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		public override void Tick()
		{
			base.Tick();
			try
			{
				if (this.DestinationConveyor.Count() < this.OutputDirection.Length) { this.UpdateDestination(); }
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void CrateArrived()
		{
			DebuggingUtils.LogInfoLine("ConveyorVerticalComponent: CrateArrived()");
			try
			{
				this.TryMoveOut();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void TryMoveOut()
		{
			try
			{
				Direction from = this.CrateData.GetSourceDirection(this.Parent.Position3i);
				DebuggingUtils.LogInfoLine($"ConveyorVerticalComponent: CrateArrived from {from}");

				switch (from)
				{
					case Direction.Up: this.TryMoveOut(Direction.Down); break;
					case Direction.Down: this.TryMoveOut(Direction.Up); break;
					default:
						DebuggingUtils.LogErrorLine($"ConveyorVerticalComponent: TryMoveOut but arrived from unsupported direction {from}");
						this.CrateStuck.Invoke();
						break;
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void TryMoveOut(Direction direction)
		{
			try
			{
				if (this.DestinationConveyor.TryGetValue(direction, out BaseConveyorObject conveyor))
				{
					BaseConveyorComponent conveyorComponent = conveyor.GetComponent<BaseConveyorComponent>();
					this.TryMoveOut(direction, conveyorComponent);
				}
				else
				{
					DebuggingUtils.LogErrorLine($"ConveyorVerticalComponent: TryMoveOut({direction}) but DestinationConveyor is null!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}
	}
}
