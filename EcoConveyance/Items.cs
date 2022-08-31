using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance
{
	[Serialized]
	[LocDisplayName("Conveyor Crate"), Tag("EcoConveyance"), Category("Hidden")]
	internal class ConveyorCrateItem : WorldObjectItem<ConveyorCrateObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor Crate");
		public override LocString DisplayDescription => Localizer.DoStr("Used to transport items over conveyors");

		public override void OnSelected(Player player)
		{
			UserInventory inventory = player.User.Inventory;
			inventory.RemoveItems(this.Type, inventory.Stacks.Sum(x => x.Item?.GetType() == this.Type ? x.Quantity : 0));
		}
	}

	#region HewnConveyor
	[Serialized]
	[MaxStackSize(100)]
	[LocDisplayName("Hewn Conveyor Roller"), Tag("EcoConveyance")]
	internal class HewnConveyorRollerItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Roller");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Hewn Conveyor Line");
	}

	[Serialized]
	[Weight(2000), MaxStackSize(10), Carried]
	[LocDisplayName("Hewn Conveyor Line"), Tag("EcoConveyance"), Tag("HewnConveyor")]
	internal class HewnConveyorItem : WorldObjectItem<HewnConveyorObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates in one direction");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Hewn Conveyor Importer"), Tag("EcoConveyance"), Tag("HewnConveyor")]
	internal class HewnConveyorImporterItem : WorldObjectItem<HewnConveyorImporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Importer");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for importing items from connected storage, packing them into crate and sends over conveyor");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Hewn Conveyor Exporter"), Tag("EcoConveyance"), Tag("HewnConveyor")]
	internal class HewnConveyorExporterItem : WorldObjectItem<HewnConveyorExporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for exporting, unpack items from arrived crates and put them into connected storage");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}
	#endregion
	#region CastIronConveyor
	[Serialized]
	[MaxStackSize(100)]
	[LocDisplayName("Cast Iron Conveyor Roller"), Tag("EcoConveyance")]
	internal class CastIronConveyorRollerItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Roller");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Cast Iron Conveyor Line");
	}

	[Serialized]
	[MaxStackSize(100)]
	[LocDisplayName("Cast Iron Profile"), Tag("EcoConveyance")]
	internal class CastIronProfileItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Profile");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Cast Iron Conveyor Line");
	}

	[Serialized]
	[Weight(2000), MaxStackSize(5), Carried]
	[LocDisplayName("Cast Iron Conveyor Frame"), Tag("EcoConveyance")]
	internal class CastIronConveyorFrameItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Frame");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Cast Iron Conveyor Line");
	}

	[Serialized]
	[Weight(3000), MaxStackSize(10), Carried]
	[LocDisplayName("Cast Iron Conveyor Line"), Tag("EcoConveyance"), Tag("CastIronConveyor")]
	internal class CastIronConveyorItem : WorldObjectItem<CastIronConveyorObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates in one direction");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}

	[Serialized]
	[Weight(5000), MaxStackSize(5), Carried]
	[LocDisplayName("Cast Iron Conveyor Importer"), Tag("EcoConveyance"), Tag("CastIronConveyor")]
	internal class CastIronConveyorImporterItem : WorldObjectItem<CastIronConveyorImporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Importer");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for importing items from connected storage, packing them into crate and sends over conveyor");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}

	[Serialized]
	[Weight(5000), MaxStackSize(5), Carried]
	[LocDisplayName("Cast Iron Conveyor Exporter"), Tag("EcoConveyance"), Tag("CastIronConveyor")]
	internal class CastIronConveyorExporterItem : WorldObjectItem<CastIronConveyorExporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for exporting, unpack items from arrived crates and put them into connected storage");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Cast Iron Conveyor Lift"), Tag("EcoConveyance"), Tag("CastIronConveyor")]
	internal class CastIronConveyorLiftItem : WorldObjectItem<CastIronConveyorLiftObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Lift");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates up or down, used to transport between floors");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Cast Iron Conveyor Lift Adapter"), Tag("EcoConveyance"), Tag("CastIronConveyor")]
	internal class CastIronConveyorLiftAdapterItem : WorldObjectItem<CastIronConveyorLiftAdapterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Cast Iron Conveyor Lift Adapter");
		public override LocString DisplayDescription => Localizer.DoStr("Entry and exit point for lift conveyors, put it at the top and bottom of lift conveyors and connect conveyor at the side");

		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
	}
	#endregion
}
