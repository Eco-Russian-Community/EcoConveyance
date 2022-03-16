using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance
{
	#region HewnConveyor
	[RequiresSkill(typeof(CarpentrySkill), 2)]
	internal class HewnConveyorRollerRecipe : RecipeFamily
	{
		public HewnConveyorRollerRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"HewnConveyorRoller",
				Localizer.DoStr("Hewn Conveyor Roller"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(HewnLogItem), 1, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<HewnConveyorRollerItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 0.5f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(CarpentrySkill));
			this.CraftMinutes = CreateCraftTimeValue(2f);
			this.Initialize(Localizer.DoStr("Hewn Conveyor Roller"), typeof(HewnConveyorRollerRecipe));
			CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
		}
	}

	[RequiresSkill(typeof(BasicEngineeringSkill), 2)]
	internal class HewnConveyorRecipe : RecipeFamily
	{
		public HewnConveyorRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"HewnConveyorLine",
				Localizer.DoStr("Hewn Conveyor Line"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(HewnConveyorRollerItem), 3, true),
					new IngredientElement("WoodBoard", 6, true),
					new IngredientElement(typeof(TallowItem), 6, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<HewnConveyorItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 0.5f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(BasicEngineeringSkill));
			this.CraftMinutes = CreateCraftTimeValue(2f);
			this.Initialize(Localizer.DoStr("Hewn Conveyor Line"), typeof(HewnConveyorRecipe));
			CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
		}
	}

	[RequiresSkill(typeof(BasicEngineeringSkill), 2)]
	internal class HewnConveyorImporterRecipe : RecipeFamily
	{
		public HewnConveyorImporterRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"HewnConveyorImporter",
				Localizer.DoStr("Hewn Conveyor Importer"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(HewnConveyorItem), 1, true),
					new IngredientElement("WoodBoard", 5, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<HewnConveyorImporterItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 0.5f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(BasicEngineeringSkill));
			this.CraftMinutes = CreateCraftTimeValue(3f);
			this.Initialize(Localizer.DoStr("Hewn Conveyor Importer"), typeof(HewnConveyorImporterRecipe));
			CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
		}
	}

	[RequiresSkill(typeof(BasicEngineeringSkill), 2)]
	internal class HewnConveyorExporterRecipe : RecipeFamily
	{
		public HewnConveyorExporterRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"HewnConveyorExporter",
				Localizer.DoStr("Hewn Conveyor Exporter"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(HewnConveyorItem), 1, true),
					new IngredientElement("WoodBoard", 5, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<HewnConveyorExporterItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 0.5f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(BasicEngineeringSkill));
			this.CraftMinutes = CreateCraftTimeValue(3f);
			this.Initialize(Localizer.DoStr("Hewn Conveyor Exporter"), typeof(HewnConveyorExporterRecipe));
			CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
		}
	}
	#endregion
	#region CastIronConveyor
	[RequiresSkill(typeof(SmeltingSkill), 2)]
	internal class CastIronConveyorRollerRecipe : RecipeFamily
	{
		public CastIronConveyorRollerRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyorRoller",
				Localizer.DoStr("Cast Iron Conveyor Roller"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(IronBarItem), 2, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorRollerItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(SmeltingSkill));
			this.CraftMinutes = CreateCraftTimeValue(4f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Roller"), typeof(CastIronConveyorRollerRecipe));
			CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
			CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
		}
	}

	[RequiresSkill(typeof(SmeltingSkill), 3)]
	internal class CastIronProfileRecipe : RecipeFamily
	{
		public CastIronProfileRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronProfile",
				Localizer.DoStr("Cast Iron Profile"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(IronBarItem), 1f, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronProfileItem>(5)
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(SmeltingSkill));
			this.CraftMinutes = CreateCraftTimeValue(1f);
			this.Initialize(Localizer.DoStr("Cast Iron Profile"), typeof(CastIronProfileRecipe));
			CraftingComponent.AddRecipe(typeof(AnvilObject), this);
		}
	}

	[RequiresSkill(typeof(SmeltingSkill), 3)]
	internal class CastIronConveyorFrameRecipe : RecipeFamily
	{
		public CastIronConveyorFrameRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyorFrame",
				Localizer.DoStr("Cast Iron Conveyor Frame"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(CastIronProfileItem), 12, true),
					new IngredientElement(typeof(ScrewsItem), 72, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorFrameItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(300, typeof(SmeltingSkill));
			this.CraftMinutes = CreateCraftTimeValue(3f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Frame"), typeof(CastIronConveyorFrameRecipe));
			CraftingComponent.AddRecipe(typeof(AnvilObject), this);
		}
	}
	#endregion
}
