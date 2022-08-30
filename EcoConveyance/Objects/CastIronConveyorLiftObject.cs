using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Mods.EcoConveyance.Components;
using Eco.Shared.IoC;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Objects
{
	[Serialized]
	[RequireComponent(typeof(ConveyorVerticalComponent))]
	[RequireComponent(typeof(PowerGridComponent))]
	[RequireComponent(typeof(PowerConsumptionComponent))]
	internal class CastIronConveyorLiftObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Cast Iron Conveyor Lift");
		public override LocString DisplayDescription => Localizer.DoStr("Transporting crates up or down, used to transport between floors");
		public virtual Type RepresentedItemType => typeof(CastIronConveyorLiftItem);

		protected override void OnCreatePreInitialize()
		{
			base.OnCreatePreInitialize();
			ConveyorVerticalComponent conveyor = this.GetComponent<ConveyorVerticalComponent>();
			conveyor.InputDirection = new Direction[] { Direction.Up, Direction.Down };
			conveyor.OutputDirection = new Direction[] { Direction.Up, Direction.Down };
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.GetComponent<PowerGridComponent>().Initialize(10, default(MechanicalPower));
			this.GetComponent<PowerConsumptionComponent>().Initialize(5);
			this.GetComponent<ConveyorVerticalComponent>().Speed = 1f;
		}
	}
}
