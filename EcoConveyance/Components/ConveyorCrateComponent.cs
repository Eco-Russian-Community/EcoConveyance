using Eco.Core.Controller;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;

namespace Eco.Mods.EcoConveyance.Components
{
	[Serialized, NoIcon]
	[RequireComponent(typeof(PublicStorageComponent))]
	internal class ConveyorCrateComponent : WorldObjectComponent
	{
		private PublicStorageComponent storage;

		public override void Initialize()
		{
			Log.WriteWarningLineLocStr("ConveyorCrateComponent: Initialize()");
			base.Initialize();
			this.storage = this.Parent.GetComponent<PublicStorageComponent>();
			this.storage.Initialize(1);
		}
	}
}
