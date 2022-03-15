﻿using Eco.Core.Items;
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
	[LocDisplayName("Conveyor Crate"), Tag("EcoConveyance"), Category("Hidden")]
	internal class ConveyorCrateItem : WorldObjectItem<ConveyorCrateObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Conveyor Crate");
		public override LocString DisplayDescription => Localizer.DoStr("Used to transport items over conveyors");
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
	#endregion
	#region СastIronConveyor
	[Serialized]
	[MaxStackSize(100)]
	[LocDisplayName("Сast Iron Conveyor Roller"), Tag("EcoConveyance")]
	internal class СastIronConveyorRollerItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Roller");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Сast Iron Conveyor Line");
	}

	[Serialized]
	[MaxStackSize(100)]
	[LocDisplayName("Сast Iron Profile"), Tag("EcoConveyance")]
	internal class СastIronProfileItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Profile");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Сast Iron Conveyor Line");
	}

	[Serialized]
	[Weight(2000), MaxStackSize(5), Carried]
	[LocDisplayName("Сast Iron Conveyor Frame"), Tag("EcoConveyance")]
	internal class СastIronConveyorFrameItem : Item
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Frame");
		public override LocString DisplayDescription => Localizer.DoStr("Used to make Сast Iron Conveyor Line");
	}

	[Serialized]
	[Weight(3000), MaxStackSize(10), Carried]
	[LocDisplayName("Сast Iron Conveyor Line"), Tag("EcoConveyance"), Tag("СastIronConveyor")]
	internal class СastIronConveyorItem : WorldObjectItem<СastIronConveyorObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Line");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates in one direction");
	}

	[Serialized]
	[Weight(5000), MaxStackSize(5), Carried]
	[LocDisplayName("Сast Iron Conveyor Importer"), Tag("EcoConveyance"), Tag("СastIronConveyor")]
	internal class СastIronConveyorImporterItem : WorldObjectItem<СastIronConveyorImporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Importer");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for importing items from connected storage, packing them into crate and sends over conveyor");
	}

	[Serialized]
	[Weight(5000), MaxStackSize(5), Carried]
	[LocDisplayName("Сast Iron Conveyor Exporter"), Tag("EcoConveyance"), Tag("СastIronConveyor")]
	internal class СastIronConveyorExporterItem : WorldObjectItem<СastIronConveyorExporterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Exporter");
		public override LocString DisplayDescription => Localizer.DoStr("Machine for exporting, unpack items from arrived crates and put them into connected storage");
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Сast Iron Conveyor Lift"), Tag("EcoConveyance"), Tag("СastIronConveyor")]
	internal class СastIronConveyorLiftItem : WorldObjectItem<СastIronConveyorLiftObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Lift");
		public override LocString DisplayDescription => Localizer.DoStr("Conveyor used for transporting crates up or down, used to transport between floors");
	}

	[Serialized]
	[Weight(4000), MaxStackSize(5), Carried]
	[LocDisplayName("Сast Iron Conveyor Lift Adapter"), Tag("EcoConveyance"), Tag("СastIronConveyor")]
	internal class СastIronConveyorLiftAdapterItem : WorldObjectItem<СastIronConveyorLiftAdapterObject>
	{
		public override LocString DisplayNamePlural => Localizer.DoStr("Сast Iron Conveyor Lift Adapter");
		public override LocString DisplayDescription => Localizer.DoStr("Entry and exit point for lift conveyors, put it at the top and bottom of lift conveyors and connect conveyor at the side");
	}
	#endregion

}
