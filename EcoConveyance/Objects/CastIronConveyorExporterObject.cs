using Eco.Core.Utils;
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
	internal class CastIronConveyorExporterObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Cast Iron Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Unpack items from arrived crates and put them into connected storage");
		public virtual Type RepresentedItemType => typeof(CastIronConveyorExporterItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			ConveyorExporterComponent conveyor = this.GetComponent<ConveyorExporterComponent>();
			conveyor.InputDirection = new Direction[] { DirectionExtensions.FacingDir(this.Rotation.RotateVector(Vector3.Back)) };
			conveyor.OutputDirection = new Direction[] { Direction.None };
		}

		protected override void Initialize()
		{
			this.GetComponent<LinkComponent>().Initialize(1);
			this.GetComponent<PowerGridComponent>().Initialize(10, default(MechanicalPower));
			this.GetComponent<PowerConsumptionComponent>().Initialize(10);
			this.GetComponent<ConveyorExporterComponent>().Speed = 1f;
			base.Initialize();
		}
	}
}
