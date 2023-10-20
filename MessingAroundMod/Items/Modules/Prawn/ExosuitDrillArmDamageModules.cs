using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Assets;

using System.Collections.Generic;

namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules.Prawn
{
    using Utility.Vehicles;
    using Utility;

    public class ExosuitDrillArmDamageModules
    {
        public class ExosuitUpgradeLevel : UpgradeLevel
        {
            /// <summary>
            /// How much additional damage does this upgrade
            /// grant to the prawn's drilling arm.
            /// </summary>
            public float damageMultiplier;
        }

        public static List<ExosuitUpgradeLevel> Levels = new List<ExosuitUpgradeLevel>()
        {
            new ExosuitUpgradeLevel()
            {
                id = "PrawnModuleDrillArmDamageMK1",
                display = "Prawn Drill Arm Damage Module MK.1",
                description = "Increases the drill arm's damage by 15%.",
                unlockedAtStart = true,

                iconTechType = TechType.ExosuitDrillArmModule,
                graphicsTechType = TechType.ExosuitDrillArmModule,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.PlasteelIngot),
                        new CraftData.Ingredient(TechType.Diamond, 4),
                        new CraftData.Ingredient(TechType.Magnetite),
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 3F,
                damageMultiplier = 1.15F
            },
            new ExosuitUpgradeLevel()
            {
                id = "PrawnModuleDrillArmDamageMK2",
                display = "Prawn Drill Arm Damage Module MK.2",
                description = "Increases the drill arm's damage by 25%.",
                unlockedAtStart = true,

                iconTechType = TechType.ExosuitDrillArmModule,
                graphicsTechType = TechType.ExosuitDrillArmModule,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.PlasteelIngot),
                        new CraftData.Ingredient(TechType.Diamond, 4),
                        new CraftData.Ingredient(TechType.Magnetite, 2),
                        new CraftData.Ingredient(TechType.UraniniteCrystal),
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 3F,
                damageMultiplier = 1.25F
            },
            new ExosuitUpgradeLevel()
            {
                id = "PrawnModuleDrillArmDamageMK3",
                display = "Prawn Drill Arm Damage Module MK.3",
                description = "Increases the drill arm's damage by 50%.",
                unlockedAtStart = true,

                iconTechType = TechType.ExosuitDrillArmModule,
                graphicsTechType = TechType.ExosuitDrillArmModule,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.PlasteelIngot),
                        new CraftData.Ingredient(TechType.Diamond, 4),
                        new CraftData.Ingredient(TechType.Magnetite, 4),
                        new CraftData.Ingredient(TechType.UraniniteCrystal, 2),
                        new CraftData.Ingredient(TechTypeHelper.Ruby, 8),
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 3F,
                damageMultiplier = 1.5F
            }
        };

        private static List<PrefabInfo> prefabInfos = new List<PrefabInfo>();
        public static PrefabInfo[] Infos { get { return prefabInfos.ToArray(); } }

        public static void Register()
        {
            foreach (var level in Levels)
            {
                RegisterExosuitUpgradeLevel(level);
            }
        }

        /// <summary>
        /// Creates and registers a new item for an exosuit upgrade level class.
        /// </summary>
        /// <param name="level">The parameters for that new item.</param>
        public static void RegisterExosuitUpgradeLevel(ExosuitUpgradeLevel level)
        {
            PrefabInfo info = PrefabInfo.WithTechType
            (
                level.id,
                level.display,
                level.description,
                unlockAtStart: level.unlockedAtStart
            )
            .WithIcon(SpriteManager.Get(level.iconTechType));

            CustomPrefab prefab = new CustomPrefab(info);

            // Sets up what the item will look like.
            prefab.SetGameObject(new CloneTemplate(info, level.graphicsTechType));

            // Sets up the recipe for that new item, how long it will
            // take to craft and where you will be able to craft it.
            prefab
                .SetRecipe(level.recipe)
                .WithFabricatorType(level.workstation)
                .WithCraftingTime(level.craftTime);

            // Sets up what type this item will be (where it can go)
            // and hooks the event to Nautilus implementation.
            prefab
                .SetVehicleUpgradeModule(EquipmentType.ExosuitModule, QuickSlotType.Passive)
                .WithOnModuleAdded(level.onAdded)
                .WithOnModuleRemoved(level.onRemoved);

            // Let's not forget to add our info to the prefab infos so we can
            // access it through out the project!
            prefabInfos.Add(info);
            // Very important: without it, the item will NEVER appear in-game.
            prefab.Register();
        }

        /// <summary>
        /// Calculates how high should the damage multiplier be for any exosuit.
        /// </summary>
        /// <returns>A float corresponding to the damage multiplier. 1.0F if
        /// there's no damage multiplier applied, the correct amount otherwise.</returns>
        public static float GetDamageMultiplier(Exosuit exosuit)
        {
            // We start at the end of the levels list and go down instead of the
            // traditional "start at 0 and go up indexes". This way we can start
            // executing logic for the highest levels upgrade and then go down
            // if none is found.
            int iterator = Levels.Count - 1;
            while (iterator > 0)
            {
                ExosuitUpgradeLevel currentLevel = Levels[iterator];
                PrefabInfo currentLevelInfo = prefabInfos[iterator];
                // Let's decrease our iterator here since we did assign
                // all variables relying on it just before.
                iterator--;

                bool hasUpgrade = VehicleUtility.HasModuleInstalled(exosuit, currentLevelInfo.TechType);
                // If the suit has the higher level upgrade, then we can
                // affirm this should be the damage multiplier applied.
                // Otherwise, we can go down one level, and check for the
                // lower level one.
                if (!hasUpgrade) continue;
                return currentLevel.damageMultiplier;
            }

            // If no upgrade was found, we will simply return 1.0F,
            // effectively not affecting the prawn's drilling arm's damage.
            return 1.0F;
        }
    }
}
