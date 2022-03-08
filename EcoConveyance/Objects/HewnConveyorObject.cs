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
	internal class HewnConveyorObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Hewn Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Transporting crates in one direction");
		public virtual Type RepresentedItemType => typeof(HewnConveyorItem);

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
	}
}
