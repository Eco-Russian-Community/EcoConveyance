using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
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
	[RequireComponent(typeof(PropertyAuthComponent))]
	internal class ConveyorVerticalEndComponent : BaseConveyorComponent
	{
		//public override void Initialize()
		//{
		//	try
		//	{
		//		this.UpdateDestination();
		//	}
		//	catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		//	base.Initialize();
		//}

		//public override void Tick()
		//{
		//	base.Tick();
		//	try
		//	{
		//		if (this.DestinationConveyor.Count() == 0 ||
		//			!this.DestinationConveyor.ContainsKey(this.OutputDirection[0]))
		//		{ this.UpdateDestination(); }
		//	}
		//	catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		//}

		protected override void CrateArrived()
		{
			DebuggingUtils.LogInfoLine("ConveyorVerticalEndComponent: CrateArrived()");
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
				DebuggingUtils.LogInfoLine($"ConveyorVerticalEndComponent: CrateArrived from {from}");

				switch (from)
				{
					case Direction.Up:
					case Direction.Down:
						this.TryMoveOut(this.OutputDirection[0]);
						break;
					default:
						if (this.DestinationConveyor.ContainsKey(Direction.Up))
						{
							this.TryMoveOut(Direction.Up);
						}
						else if (this.DestinationConveyor.ContainsKey(Direction.Down))
						{
							this.TryMoveOut(Direction.Down);
						}
						else
						{
							DebuggingUtils.LogErrorLine($"ConveyorVerticalEndComponent: TryMoveOut but DestinationConveyor is null!");
							this.UpdateDestination();
							this.CrateStuck.Invoke();
						}
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
					DebuggingUtils.LogErrorLine($"ConveyorVerticalEndComponent: TryMoveOut({direction}) but DestinationConveyor is null!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}
	}
}
