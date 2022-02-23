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
	[RequireComponent(typeof(ConveyorVerticalEndComponent))]
	internal class ConveyorVerticalEndObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Conveyor Vertical End");
		public virtual Type RepresentedItemType => typeof(ConveyorVerticalEndItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			this.GetComponent<ConveyorVerticalEndComponent>().OutputDirection = new Direction[] { DirectionExtensions.FacingDir(this.Rotation.Forward), Direction.Up, Direction.Down };
		}
	}
}
