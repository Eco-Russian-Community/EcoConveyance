﻿using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
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
	[RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
	[RequireComponent(typeof(PropertyAuthComponent))]
	[RequireComponent(typeof(PowerGridComponent))]
	[RequireComponent(typeof(PowerConsumptionComponent))]
	internal class HewnConveyorObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Hewn Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Transporting crates in one direction");
		public virtual Type RepresentedItemType => typeof(HewnConveyorItem);

		protected override void OnCreatePreInitialize()
		{
			base.OnCreatePreInitialize();
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
			this.GetComponent<PowerConsumptionComponent>().Initialize(4);
			this.GetComponent<ConveyorComponent>().Speed = 0.5f;
		}
	}
}
