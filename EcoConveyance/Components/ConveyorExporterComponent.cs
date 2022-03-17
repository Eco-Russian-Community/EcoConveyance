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
	[RequireComponent(typeof(OnOffComponent))]
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
					this._op = true;
					ConveyorCrateObject crate = this.CrateData.Crate;
					Inventory crateInventory = crate.Storage.Inventory;
					if (crateInventory.IsEmpty) { this.DestroyCrate(); this._op = false; return; } //Get empty crate, destroy it

					IEnumerable<StorageComponent> linkedStorages = this.link.GetEnabledLinkedComponents(this.Parent.Creator);
					if (linkedStorages.Count() == 0) { this.CrateStuck.Invoke(); return; } //No storage linked, try again

					IEnumerable<ItemStack> stacks = crateInventory.NonEmptyStacks;
					foreach (ItemStack stack in stacks)
					{
						foreach (StorageComponent storage in linkedStorages)
						{
							Core.Utils.Result result = crateInventory.TryMoveItems<Item>(stack.Item.Type, stack.Quantity, storage.Inventory);
							if (result.Success) { break; }
						}
					}
					//Destroy crate if it empty or try empty it again
					if (crateInventory.IsEmpty) { this.DestroyCrate(); } else { this.CrateStuck.Invoke(); }
					this._op = false;
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void TryMoveOut(Direction direction){ throw new NotImplementedException(); }
	}
}
