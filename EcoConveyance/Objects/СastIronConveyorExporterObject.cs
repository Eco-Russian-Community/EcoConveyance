﻿using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Components;
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
	[RequireComponent(typeof(ConveyorExporterComponent))]
	[RequireComponent(typeof(SolidGroundComponent))]
	[RequireComponent(typeof(PowerGridComponent))]
	[RequireComponent(typeof(PowerConsumptionComponent))]
	internal class СastIronConveyorExporterObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Сast Iron Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Unpack items from arrived crates and put them into connected storage");
		public virtual Type RepresentedItemType => typeof(СastIronConveyorExporterItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			Direction facing = DirectionExtensions.FacingDir(this.Rotation.RotateVector(Vector3.Back));
			List<Direction> list = new List<Direction>() {
				Direction.Left,
				Direction.Right,
				Direction.Back,
				Direction.Forward
			};
			list.Remove(facing);
			ConveyorExporterComponent conveyor = this.GetComponent<ConveyorExporterComponent>();
			conveyor.InputDirection = list.ToArray();
			conveyor.OutputDirection = new Direction[] { facing };
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.GetComponent<LinkComponent>().Initialize(1);
			this.GetComponent<PowerGridComponent>().Initialize(10, default(MechanicalPower));
			this.GetComponent<PowerConsumptionComponent>().Initialize(10);
		}
	}
}
