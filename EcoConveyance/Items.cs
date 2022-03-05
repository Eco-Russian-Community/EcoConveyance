using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Shared.Localization;
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
	[Weight(2000), MaxStackSize(10), Carried]
	[LocDisplayName("Hewn Conveyor Line"), Tag("EcoConveyance"), Tag("HewnConveyor")]
	internal class HewnConveyorItem : WorldObjectItem<HewnConveyorObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates in one direction");
	}

	[Serialized]
	[LocDisplayName("Vertical Conveyor"), Tag("EcoConveyance")]
	internal class ConveyorVerticalItem : WorldObjectItem<ConveyorVerticalObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Vertical Conveyor");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates up or down, used to transport between floors");
	}

	[Serialized]
	[LocDisplayName("Vertical Conveyor End"), Tag("EcoConveyance")]
	internal class ConveyorVerticalEndItem : WorldObjectItem<ConveyorVerticalEndObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Vertical Conveyor End");
		public override LocString DisplayDescription => Localizer.DoStr("Entry and exit point for vertical conveyors, put it at the top and bottom of vertical conveyors and connect normal conveyor at the side");
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Hewn Conveyor Importer"), Tag("EcoConveyance"), Tag("HewnConveyor")]
	internal class HewnConveyorImporterItem : WorldObjectItem<HewnConveyorImporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Importer");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for importing items from connected storage, packing them into crate and sends over conveyor");
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Hewn Conveyor Exporter"), Tag("EcoConveyance"), Tag("HewnConveyor")]
	internal class HewnConveyorExporterItem : WorldObjectItem<HewnConveyorExporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for exporting, unpack items from arrived crates and put them into connected storage");
	}

	[Serialized]
	[LocDisplayName("Conveyor Crate"), Tag("EcoConveyance"), Category("Hidden")]
	internal class ConveyorCrateItem : WorldObjectItem<ConveyorCrateObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor Crate");
		public override LocString DisplayDescription => Localizer.DoStr("Used to transport items over conveyors");
	}

	[Serialized]
	[MaxStackSize(100)]
	[LocDisplayName("Hewn Conveyor Roller"), Tag("EcoConveyance")]
	internal class HewnConveyorRollerItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Hewn Conveyor Roller");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Hewn Conveyor Line");
	}
}
