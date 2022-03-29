using Eco.Core.Controller;
using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.World.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Components
{
	[Serialized]
	internal class ConveyorComponent : BaseConveyorComponent
	{
		public override void Initialize()
		{
			try
			{
				this.UpdateDestination();
				this.UpdateVisual();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			base.Initialize();
		}

		
		//public override void Tick()
		//{
		//	base.Tick();
		//	try
		//	{
		//		if (this.DestinationConveyor.Count() < this.OutputDirection.Length) { this.UpdateDestination(); this.UpdateVisual(); }
		//	}
		//	catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		//}

		protected override void CrateArrived()
		{
			//DebuggingUtils.LogInfoLine("ConveyorComponent: CrateArrived()");
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
				if (this.DestinationConveyor.ContainsKey(this.OutputDirection[0]))
				{
					this.TryMoveOut(this.OutputDirection[0]);
				}
				else
				{
					DebuggingUtils.LogErrorLine($"ConveyorComponent: TryMoveOut but DestinationConveyor is null!");
					this.CrateStuck.Invoke();
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
					DebuggingUtils.LogErrorLine($"ConveyorComponent: TryMoveOut({direction}) but DestinationConveyor is null!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void OnMovedOut(BaseConveyorComponent conveyor)
		{
			base.OnMovedOut(conveyor);
		}
	}
}
