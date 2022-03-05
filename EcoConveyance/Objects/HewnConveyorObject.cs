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
	[RequireComponent(typeof(ConveyorComponent))]
	[RequireComponent(typeof(SolidGroundComponent))]
	internal class HewnConveyorObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Conveyor");
		public override LocString DisplayDescription => Localizer.DoStr("Transporting crates in one direction");
		public virtual Type RepresentedItemType => typeof(HewnConveyorItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			this.GetComponent<ConveyorComponent>().OutputDirection = new Direction[] { DirectionExtensions.FacingDir(this.Rotation.Forward) };
		}
	}
}
