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
	[RequiresSkill(typeof(MechanicsSkill), 2)]
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
					new IngredientElement(typeof(IronBarItem), 3, true),
					new IngredientElement("Fabric", 1, true),
					new IngredientElement("Gear", 1, true),
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorRollerItem>(2)
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(4f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Roller"), typeof(CastIronConveyorRollerRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 2)]
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
					new IngredientElement(typeof(IronBarItem), 1, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronProfileItem>(5)
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(100, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(1f);
			this.Initialize(Localizer.DoStr("Cast Iron Profile"), typeof(CastIronProfileRecipe));
			CraftingComponent.AddRecipe(typeof(ScrewPressObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 3)]
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
					new IngredientElement(typeof(ScrewsItem), 24, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorFrameItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(300, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(3f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Frame"), typeof(CastIronConveyorFrameRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 2)]
	internal class CastIronConveyorRecipe : RecipeFamily
	{
		public CastIronConveyorRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyor",
				Localizer.DoStr("Cast Iron Conveyor Line"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(CastIronProfileItem), 2, true),
					new IngredientElement(typeof(CastIronConveyorRollerItem), 3, true),
					new IngredientElement(typeof(OilItem), 3, true),
					new IngredientElement(typeof(ScrewsItem), 7, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(5f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Line"), typeof(CastIronConveyorRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 2)]
	internal class CastIronConveyorImporterRecipe : RecipeFamily
	{
		public CastIronConveyorImporterRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyorImporter",
				Localizer.DoStr("Cast Iron Conveyor Importer"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(CastIronConveyorItem), 1, true),
					new IngredientElement(typeof(IronPlateItem), 5, true),
					new IngredientElement(typeof(ScrewsItem), 24, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorImporterItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(5f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Importer"), typeof(CastIronConveyorImporterRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 2)]
	internal class CastIronConveyorExporterRecipe : RecipeFamily
	{
		public CastIronConveyorExporterRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyorExporter",
				Localizer.DoStr("Cast Iron Conveyor Exporter"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(CastIronConveyorItem), 1, true),
					new IngredientElement(typeof(IronPlateItem), 5, true),
					new IngredientElement(typeof(ScrewsItem), 24, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorExporterItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(5f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Exporter"), typeof(CastIronConveyorExporterRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 3)]
	internal class CastIronConveyorLiftRecipe : RecipeFamily
	{
		public CastIronConveyorLiftRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyorLift",
				Localizer.DoStr("Cast Iron Conveyor Lift"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(CastIronConveyorItem), 1, true),
					new IngredientElement(typeof(CastIronConveyorFrameItem), 1, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorLiftItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(5f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Lift"), typeof(CastIronConveyorLiftRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}

	[RequiresSkill(typeof(MechanicsSkill), 3)]
	internal class CastIronConveyorLiftAdapterRecipe : RecipeFamily
	{
		public CastIronConveyorLiftAdapterRecipe()
		{
			Recipe recipe = new Recipe();
			recipe.Init(
				"CastIronConveyorLiftAdapter",
				Localizer.DoStr("Cast Iron Conveyor Lift Adapter"),
				new List<IngredientElement>
				{
					new IngredientElement(typeof(CastIronConveyorLiftItem), 1, true)
				},
				new List<CraftingElement>
				{
					new CraftingElement<CastIronConveyorLiftAdapterItem>()
				});
			this.Recipes = new List<Recipe> { recipe };
			this.ExperienceOnCraft = 1f;
			this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(MechanicsSkill));
			this.CraftMinutes = CreateCraftTimeValue(5f);
			this.Initialize(Localizer.DoStr("Cast Iron Conveyor Lift Adapter"), typeof(CastIronConveyorLiftAdapterRecipe));
			CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
		}
	}
	#endregion
}
