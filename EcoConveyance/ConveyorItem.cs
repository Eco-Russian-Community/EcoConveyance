using Eco.Gameplay.Items;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance
{
	[Serialized]
	internal class ConveyorItem : WorldObjectItem<ConveyorObject>
	{

	}

	[Serialized]
	internal class ConveyorVerticalItem : WorldObjectItem<ConveyorVerticalObject>
	{

	}

	[Serialized]
	internal class ConveyorVerticalEndItem : WorldObjectItem<ConveyorVerticalEndObject>
	{

	}

	[Serialized]
	internal class ConveyorImporterItem : WorldObjectItem<ConveyorImporterObject>
	{

	}

	[Serialized]
	internal class ConveyorExporterItem : WorldObjectItem<ConveyorExporterObject>
	{

	}

	[Serialized]
	internal class ConveyorCrateItem : WorldObjectItem<ConveyorCrateObject>
	{

	}
}
