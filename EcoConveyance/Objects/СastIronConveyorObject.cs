using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Components;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;

namespace Eco.Mods.EcoConveyance.Objects
{
	[Serialized]
	[RequireComponent(typeof(ConveyorComponent))]
	[RequireComponent(typeof(SolidGroundComponent))]
	[RequireComponent(typeof(PowerGridComponent))]
	[RequireComponent(typeof(PowerConsumptionComponent))]
	internal class СastIronConveyorObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Сast Iron Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Transporting crates in one direction");
		public virtual Type RepresentedItemType => typeof(СastIronConveyorItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			ConveyorComponent conveyor = this.GetComponent<ConveyorComponent>();
			conveyor.InputDirection = new Direction[] {
				Direction.Left,
				Direction.Right,
				Direction.Back,
				Direction.Forward
			};
			conveyor.OutputDirection = new Direction[] { DirectionExtensions.FacingDir(this.Rotation.Forward) };
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.GetComponent<PowerGridComponent>().Initialize(10, default(MechanicalPower));
			this.GetComponent<PowerConsumptionComponent>().Initialize(2);
		}
	}
}
