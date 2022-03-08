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
	internal class ConveyorVerticalObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Vertical Conveyor");
		public override LocString DisplayDescription => Localizer.DoStr("Transporting crates up or down, used to transport between floors");
		public virtual Type RepresentedItemType => typeof(ConveyorVerticalItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			ConveyorVerticalComponent conveyor = this.GetComponent<ConveyorVerticalComponent>();
			conveyor.InputDirection = new Direction[] { Direction.Up, Direction.Down };
			conveyor.OutputDirection = new Direction[] { Direction.Up, Direction.Down };
		}
	}
}
