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
	internal class ConveyorImporterComponent : BaseConveyorComponent
	{
		public override bool CanReceive => false;
		private readonly PeriodicUpdate workDelay = new PeriodicUpdate(5);
		private LinkComponent link;

		public override void Initialize()
		{
			try
			{
				this.link = this.Parent.GetComponent<LinkComponent>();
				this.UpdateDestination();
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
			base.Initialize();
		}

		public override void Tick()
		{
			base.Tick();
			try
			{
				if (this.DestinationConveyor.Count() < this.OutputDirection.Length) { this.UpdateDestination(); }
				if(!this.Parent.Enabled || IsShutdown) { return; }
				if (this.CrateData != null) { this.TryMoveOut(); }
				else if(this.workDelay.DoUpdate)
				{
					IEnumerable<StorageComponent> linkedStorages = this.link.GetEnabledLinkedComponents(this.Parent.Creator);
					if (linkedStorages.Count() == 0) { return; }

					foreach (StorageComponent storage in linkedStorages)
					{
						ItemStack stack = StorageUtils.TryGetItemStack(storage);
						if (stack != null && stack.Item != null && this.CrateData == null)
						{
							ConveyorCrateObject crate = SpawnCrate();
							this.CrateData = new CrateData(crate, this.Parent);
							Core.Utils.Result result = storage.Inventory.TryMoveItems<Item>(stack.Item.Type, stack.Quantity, crate.Storage.Storage);
							if (result.Success) { break; }
							if (result.Failed) { this.DestroyCrate(); } //TODO: Probably make crate obj reusable
						}
					}
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		private ConveyorCrateObject SpawnCrate()
		{
			ConveyorCrateObject obj = (ConveyorCrateObject)Activator.CreateInstance(typeof(ConveyorCrateObject), true);
			if (obj == null) { return null; }
			DebuggingUtils.LogInfoLine($"ConveyorImporterComponent: SpawnCrate({obj})");
			return (ConveyorCrateObject)ServiceHolder<IWorldObjectManager>.Obj.Add(obj, this.Parent.Creator, this.Parent.Position, Quaternion.Identity);
		}

		protected override void TryMoveOut()
		{
			try
			{
				if (this.DestinationConveyor.ContainsKey(this.OutputDirection[0]))
				{
					this.TryMoveOut(this.OutputDirection[0]);
				}
				else
				{
					DebuggingUtils.LogErrorLine($"ConveyorComponent: TryMoveOut but DestinationConveyor is null!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}

		protected override void TryMoveOut(Direction direction)
		{
			try
			{
				if (this.DestinationConveyor.TryGetValue(direction, out BaseConveyorObject conveyor))
				{
					BaseConveyorComponent conveyorComponent = conveyor.GetComponent<BaseConveyorComponent>();
					this.TryMoveOut(direction, conveyorComponent);
				}
				else
				{
					DebuggingUtils.LogErrorLine($"ConveyorComponent: TryMoveOut({direction}) but DestinationConveyor is null!");
					this.CrateStuck.Invoke();
				}
			}
			catch (Exception ex) { Log.WriteErrorLineLocStr(ex.ToString()); }
		}
		protected override void CrateArrived() { throw new NotImplementedException(); }
	}
}
