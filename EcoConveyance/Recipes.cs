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
}
