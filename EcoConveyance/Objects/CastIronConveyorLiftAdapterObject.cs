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
	[RequireComponent(typeof(PowerGridComponent))]
	[RequireComponent(typeof(PowerConsumptionComponent))]
	internal class CastIronConveyorLiftAdapterObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Cast Iron Conveyor Lift Adapter");
		public override LocString DisplayDescription => Localizer.DoStr("Entry and exit point for lift conveyors, put it at the top and bottom of lift conveyors and connect conveyor at the side");
		public virtual Type RepresentedItemType => typeof(CastIronConveyorLiftAdapterItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			Direction facing = DirectionExtensions.FacingDir(this.Rotation.Forward);
			ConveyorVerticalEndComponent conveyor = this.GetComponent<ConveyorVerticalEndComponent>();
			conveyor.InputDirection = new Direction[] { facing.Inverse(), Direction.Up, Direction.Down };
			conveyor.OutputDirection = new Direction[] { facing, Direction.Up, Direction.Down };
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.GetComponent<PowerGridComponent>().Initialize(10, default(MechanicalPower));
			this.GetComponent<PowerConsumptionComponent>().Initialize(10);
			this.GetComponent<ConveyorVerticalEndComponent>().Speed = 1f;
		}
	}
}
