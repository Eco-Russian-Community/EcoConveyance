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
	using World = Eco.World.World;

	[Serialized]
	[RequireComponent(typeof(LinkComponent))]
	[RequireComponent(typeof(InOutLinkedInventoriesComponent))]
	[RequireComponent(typeof(PropertyAuthComponent))]
	[RequireComponent(typeof(OnOffComponent))]
	internal class ConveyorExporterComponent : BaseConveyorComponent, IOperatingWorldObjectComponent
	{
		public bool Operating => this._op;
		protected bool _op = false;
		private LinkComponent link;

		public override void Initialize()
		{
			try
			{
				this.link = this.Parent.GetComponent<LinkComponent>();
				this.link.OnLinked.Add(this.OnInventoryLinked);
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			base.Initialize();
		}

		private void OnInventoryLinked(StorageComponent storage)
		{
			if (storage == null) { return; }
			List<Vector3i> storagePlacement = Vector3iUtils.OccupancyWorldPosition(storage.Parent);
			IEnumerable<Vector3i> ourPositions = Vector3iUtils.OccupancyWorldPosition(this.Parent);
			float minDistance = float.MaxValue;
			foreach(Vector3i pos in ourPositions)
			{
				foreach(Vector3i storagePos in storagePlacement)
				{
					float dst = World.WrappedDistance(pos, storagePos);
					if(dst < minDistance) { minDistance = dst; }
				}
			}

			if(minDistance > 1f)
			{
				IEnumerable<LinkComponent> links = this.link.GetAuthorizedLinkedObjects(this.Parent.Creator);
				foreach (LinkComponent link in links)
				{
					if (storage.Parent.Equals(link.Parent))
					{
						DebuggingUtils.LogErrorLine($"ConveyorExporterComponent: OnInventoryLinked() {storage.Parent} with minDistance={minDistance}");
						this.link.SetObjectEnabled(this.Parent.Creator, storage, false);
					}
				}
			}
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
