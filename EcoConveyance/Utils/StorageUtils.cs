using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Utils
{
	internal static class StorageUtils
	{
		public static ItemStack TryGetItemStack(StorageComponent storage)
		{
			Inventory inventory = storage.Inventory;
			if (inventory == null) { return null; }
			return TryGetItemStack(inventory);
		}

		public static ItemStack TryGetItemStack(Inventory inventory)
		{
			IEnumerable<ItemStack> stacks = inventory.NonEmptyStacks;
			if (stacks == null || stacks.Count() == 0) { return null; }
			return TryGetItemStack(stacks);
		}

		public static ItemStack TryGetItemStack(IEnumerable<ItemStack> stacks)
		{
			foreach (ItemStack stack in stacks)
			{
				if (stack != null && !stack.Empty()) { return stack; }
			}
			return null;
		}
	}
}
