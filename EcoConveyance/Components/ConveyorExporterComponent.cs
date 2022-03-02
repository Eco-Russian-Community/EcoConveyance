using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Mods.EcoConveyance.Objects;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.IoC;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Components
{
	[Serialized]
	[RequireComponent(typeof(LinkComponent))]
	[RequireComponent(typeof(PropertyAuthComponent))]
	internal class ConveyorExporterComponent : BaseConveyorComponent
	{
		private LinkComponent link;

		public override void Initialize()
		{
			base.Initialize();
			try
			{
				this.link = this.Parent.GetComponent<LinkComponent>();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void CrateArrived()
		{
			DebuggingUtils.LogInfoLine("ConveyorExporterComponent: CrateArrived()");
			try
			{
				this.CrateStuck.Invoke();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void TryMoveOut()
		{
			DebuggingUtils.LogInfoLine("ConveyorExporterComponent: TryMoveOut()");
			try
			{
				if (this.CrateData != null && this.CrateData.Crate.Storage != null)
				{
					ConveyorCrateObject crate = this.CrateData.Crate;
					if (crate.Storage.Inventory.IsEmpty) { this.DestroyCrate(); return; } //Get empty crate, destroy it

					ItemStack stack = StorageUtils.TryGetItemStack(crate.Storage);
					if (stack == null || stack.Item == null) { this.DestroyCrate(); return; } //Get empty crate, destroy it

					IEnumerable<StorageComponent> linkedStorages = this.link.GetEnabledLinkedComponents(this.Parent.Creator);
					if (linkedStorages.Count() == 0) { this.CrateStuck.Invoke(); return; }
					foreach (StorageComponent storage in linkedStorages)
					{
						Core.Utils.Result result = crate.Storage.Inventory.TryMoveItems<Item>(stack.Item.Type, stack.Quantity, storage.Inventory);
						if (result.Success) { this.DestroyCrate(); break; }
					}
				}
				if(this.CrateData != null) { this.CrateStuck.Invoke(); }
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void TryMoveOut(Direction direction){ throw new NotImplementedException(); }
	}
}
