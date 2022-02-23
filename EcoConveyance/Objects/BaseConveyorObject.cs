using Eco.Core.Utils;
using Eco.Gameplay.Objects;
using Eco.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Objects
{
	internal abstract class BaseConveyorObject : WorldObject
	{
		public readonly ThreadSafeAction<BaseConveyorObject> OnDestroy = new ThreadSafeAction<BaseConveyorObject>();

		public override void Destroy()
		{
			this.OnDestroy.Invoke(this);
			base.Destroy();
		}
	}
}
