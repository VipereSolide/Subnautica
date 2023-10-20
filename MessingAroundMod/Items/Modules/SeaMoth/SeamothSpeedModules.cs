using System.Collections.Generic;

using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Assets;

namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules.SeamothModules
{
    using Utility.Vehicles;

    public class SeamothSpeedModules
    {
        public class SeamothUpgradeLevel : UpgradeLevel
        {
            /// <summary>
            /// How much faster will the seamoth go using this upgrade?
            /// </summary>
            public float speedMultiplier;
        }

        public static List<SeamothUpgradeLevel> Levels = new List<SeamothUpgradeLevel>()
        {
            new SeamothUpgradeLevel()
            {
                id = "SeamothSpeedModuleMK1",
                display = "Seamoth Speed Module MK.1",
                description = "Using lubricant on the rotor and changing for better wiring increases the seamoth's speed by 15%.",
                unlockedAtStart = true,

                iconTechType = TechType.MapRoomUpgradeScanSpeed,
                graphicsTechType = TechType.MapRoomUpgradeScanSpeed,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.WiringKit),
                        new CraftData.Ingredient(TechType.Lubricant),
                        new CraftData.Ingredient(TechType.Lithium),
                    }
                },
                workstation = CraftTree.Type.SeamothUpgrades,
                craftTime = 5F,
                speedMultiplier = 1.15F
            },
            new SeamothUpgradeLevel()
            {
                id = "SeamothSpeedModuleMK2",
                display = "Seamoth Speed Module MK.2",
                description = "Magnetic properties of the magnetite are used as a second force to rotate the rotor, increasing the seamoth's speed by 30%. (30 seconds to create)",
                unlockedAtStart = true,

                iconTechType = TechType.MapRoomUpgradeScanSpeed,
                graphicsTechType = TechType.MapRoomUpgradeScanSpeed,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.Magnetite),
                        new CraftData.Ingredient(TechType.WhiteMushroom, 2),
                        new CraftData.Ingredient(TechType.Lithium, 4),
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 30F,
                speedMultiplier = 1.25F
            },
            new SeamothUpgradeLevel()
            {
                id = "SeamothSpeedModuleMK3",
                display = "Seamoth Speed Module MK.3",
                description = "Using benzene as the libricant, any friction between the palm of the motor and the water have been reduced greatly thus increasing the seamoth's speed by 60%. (1 minute to create)",
                unlockedAtStart = true,

                iconTechType = TechType.MapRoomUpgradeScanSpeed,
                graphicsTechType = TechType.MapRoomUpgradeScanSpeed,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.Magnetite, 4),
                        new CraftData.Ingredient(TechType.Lithium, 4),
                        new CraftData.Ingredient(TechType.Benzene, 1),
                        new CraftData.Ingredient(TechType.ComputerChip),
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 60F,
                speedMultiplier = 1.25F
            },
            new SeamothUpgradeLevel()
            {
                id = "SeamothSpeedModuleMK4",
                display = "Seamoth Speed Module MK.4",
                description = "Powers the engine using chimical reactions between magnetite, diamonds and ion crystals, thus providing a 120% speed increase to the seamoth. (2 minutes to create)",
                unlockedAtStart = true,

                iconTechType = TechType.MapRoomUpgradeScanSpeed,
                graphicsTechType = TechType.MapRoomUpgradeScanSpeed,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.PrecursorIonCrystal, 1),
                        new CraftData.Ingredient(TechType.Lithium, 4),
                        new CraftData.Ingredient(TechType.Magnetite, 4),
                        new CraftData.Ingredient(TechType.Diamond, 8),
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 120F,
                speedMultiplier = 2.2F
            }
        };

        private static List<PrefabInfo> prefabInfos = new List<PrefabInfo>();
        public static PrefabInfo[] Infos { get { return prefabInfos.ToArray(); } }


        public static void Register()
        {
            foreach (var level in Levels)
            {
                RegisterSeamothUpgradeLevel(level);
            }
        }

        public static void RegisterSeamothUpgradeLevel(SeamothUpgradeLevel level)
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
            prefab.SetGameObject(new CloneTemplate(info, level.graphicsTechType));
            prefab
                .SetRecipe(level.recipe)
                .WithFabricatorType(level.workstation)
                .WithCraftingTime(level.craftTime)
                .WithStepsToFabricatorTab("SeamothModules");
            prefab
                .SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.Passive)
                .WithOnModuleAdded(level.onAdded)
                .WithOnModuleRemoved(level.onRemoved);

            prefabInfos.Add(info);
            prefab.Register();
        }

        public static float GetSpeedMultiplier(Vehicle vehicle)
        {
            int iterator = Levels.Count - 1;
            while (iterator > 0)
            {
                SeamothUpgradeLevel currentLevel = Levels[iterator];
                PrefabInfo currentLevelInfo = prefabInfos[iterator];
                iterator--;

                bool hasUpgrade = VehicleUtility.HasModuleInstalled(vehicle, currentLevelInfo.TechType);

                if (!hasUpgrade) continue;
                return currentLevel.speedMultiplier;
            }

            return 1.0F;
        }
    }
}
