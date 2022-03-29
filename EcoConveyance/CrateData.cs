using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Components;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
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

		public CrateData ChangeSource(BaseConveyorComponent source) => this.ChangeSource((BaseConveyorObject)source.Parent);
		public CrateData ChangeSource(BaseConveyorObject source)
		{
			this._source = source;
			return this;
		}

		public Direction GetSourceDirection(Vector3i thisPosition)
		{
			if(this.Source == null) { return Direction.Unknown; }
			return DirectionUtils.GetDeltaDirection(this.Source.Position3i, thisPosition);
		}

		public override string ToString()
		{
			return $"Crate {this.Crate} from {this.Source}";
		}
	}
}
