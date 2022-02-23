using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance
{
	[Serialized]
	internal sealed class CrateData
	{
		[Serialized] private WorldObjectHandle _crate;
		[Serialized] private WorldObjectHandle _source;

		public ConveyorCrateObject Crate => (ConveyorCrateObject)this._crate;
		public BaseConveyorObject Source => (BaseConveyorObject)this._source;

		private CrateData() { }
		public CrateData(ConveyorCrateObject crate, WorldObject source) : this(crate, (BaseConveyorObject)source) { }
		public CrateData(ConveyorCrateObject crate, BaseConveyorObject source)
		{
			this._crate = crate;
			this._source = source;
		}

		public Direction GetSourceDirection(Vector3i thisPosition) => WorldPosition3i.GetDelta(this.Source.Position3i, thisPosition).ToDir();

		public override string ToString()
		{
			return $"Crate {this.Crate} from {this.Source}";
		}
	}
}
