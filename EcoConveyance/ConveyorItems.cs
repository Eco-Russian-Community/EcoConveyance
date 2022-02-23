using Eco.Core.Items;
using Eco.Gameplay.Items;
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
	[LocDisplayName("Conveyor"), Tag("EcoConveyance")]
	internal class ConveyorItem : WorldObjectItem<ConveyorObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor");
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
	[LocDisplayName("Conveyor Importer"), Tag("EcoConveyance")]
	internal class ConveyorImporterItem : WorldObjectItem<ConveyorImporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor Importer");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for importing items from connected storage, packing them into crate and sends over conveyor");
	}

	[Serialized]
	[LocDisplayName("Conveyor Exporter"), Tag("EcoConveyance")]
	internal class ConveyorExporterItem : WorldObjectItem<ConveyorExporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for exporting, unpack items from arrived crates and put them into connected storage");
	}

	[Serialized]
	[LocDisplayName("Conveyor Crate"), Tag("EcoConveyance"), Category("Hidden")]
	internal class ConveyorCrateItem : WorldObjectItem<ConveyorCrateObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor Crate");
		public override LocString DisplayDescription => Localizer.DoStr("Used to transport items over conveyors");
		public override bool PreventUseWithCarriedItems => true;
	}
}
