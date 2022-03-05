using Eco.Core.Controller;
using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.GameActions;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Wires;
using Eco.Mods.EcoConveyance.Components;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;
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
	[RequireComponent(typeof(PublicStorageComponent))]
	[RequireComponent(typeof(PropertyAuthComponent))]
	internal class ConveyorCrateObject : WorldObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Conveyor Crate");
		public override LocString DisplayDescription => Localizer.DoStr("Used to transport items over conveyors");
		public virtual Type RepresentedItemType => typeof(ConveyorCrateItem);

		public readonly ThreadSafeAction OnDestroy = new ThreadSafeAction();
		public PublicStorageComponent Storage { get; private set; }

		//protected override void OnCreate()
		//{
		//	Log.WriteWarningLineLocStr("ConveyorCrateObject: OnCreate()");
		//	base.OnCreate();

		//	this.OnOperatingChange.Add(OnOperatingChange);
		//	void OnOperatingChange()
		//	{
		//		Log.WriteWarningLineLocStr("ConveyorCrateObject: OnOperatingChange()");
		//	}
		//}

		protected override void PostInitialize()
		{
			DebuggingUtils.LogInfoLine($"ConveyorCrateObject: PostInitialize({this})");
			base.PostInitialize();
			this.Storage = this.GetComponent<PublicStorageComponent>();
			this.Storage.Initialize(1);
			this.Storage.HiddenFromUI = true;
		}

		public override void Destroy()
		{
			DebuggingUtils.LogInfoLine($"ConveyorCrateObject: Destroy({this})");
			this.OnDestroy.Invoke();
			base.Destroy();
		}

		public override InteractResult OnActInteract(InteractionContext context) => InteractResult.NoOp;
	}
}
