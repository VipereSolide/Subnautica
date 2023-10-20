using System.Collections.Generic;
using System;

using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Assets;

using UnityEngine;

namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules.SeamothModules
{
    using System.Runtime.CompilerServices;
    using Utility.Vehicles;
    using VipereSolide.Subnautica.MessingAroundMod.Utility.Math;

    public class SeamothDefenseModule
    {
        public class SeamothDefenseUpgradeLevel : UpgradeLevel { }

        public static List<SeamothDefenseUpgradeLevel> Levels = new List<SeamothDefenseUpgradeLevel>()
        {
            new SeamothDefenseUpgradeLevel()
            {
                id = "SeamothDefenseModuleMK1",
                display = "Seamoth Defense Module MK.1",
                description = "Creates a huge energy wave around the seamoth, projecting any creature in a 15 meters radius around the vehicle.",
                unlockedAtStart = true,

                iconTechType = TechType.SeamothElectricalDefense,
                graphicsTechType = TechType.SeamothElectricalDefense,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.WiringKit),
                        new CraftData.Ingredient(TechType.PrecursorIonCrystal),
                        new CraftData.Ingredient(TechType.Magnetite, 4),
                    }
                },
                workstation = CraftTree.Type.SeamothUpgrades,
                craftTime = 5F,

                onUsed = (Vehicle vehicle, int slotId, float unknown1, float unknown2) => OnUpgradeUsed(vehicle, slotId, 15, 0.05F),
                energyCost = 15,
                cooldown = 30,
                maxCharge = 25
            },
            new SeamothDefenseUpgradeLevel()
            {
                id = "SeamothDefenseModuleMK2",
                display = "Seamoth Defense Module MK.2",
                description = "Creates a huge energy wave around the seamoth, projecting any creature in a 20 meters radius around the vehicle. (2 minutes to craft)",
                unlockedAtStart = true,

                iconTechType = TechType.SeamothElectricalDefense,
                graphicsTechType = TechType.SeamothElectricalDefense,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.WiringKit),
                        new CraftData.Ingredient(TechType.PrecursorIonCrystal, 3),
                        new CraftData.Ingredient(TechType.Magnetite, 4),
                        new CraftData.Ingredient(TechType.PrecursorKey_Purple, 1),
                    }
                },
                workstation = CraftTree.Type.SeamothUpgrades,
                craftTime = 120F,

                onUsed = (Vehicle vehicle, int slotId, float unknown1, float unknown2) => OnUpgradeUsed(vehicle, slotId, 20, 0.1F),
                energyCost = 18,
                cooldown = 22,
                maxCharge = 25
            },
            new SeamothDefenseUpgradeLevel()
            {
                id = "SeamothDefenseModuleMK3",
                display = "Seamoth Defense Module MK.3",
                description = "Creates a huge energy wave around the seamoth, projecting any creature in a 25 meters radius around the vehicle. (4 minutes to craft)",
                unlockedAtStart = true,

                iconTechType = TechType.SeamothElectricalDefense,
                graphicsTechType = TechType.SeamothElectricalDefense,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.WiringKit),
                        new CraftData.Ingredient(TechType.PrecursorIonCrystal, 3),
                        new CraftData.Ingredient(TechType.Magnetite, 4),
                        new CraftData.Ingredient(TechType.PrecursorKey_Purple, 1),
                        new CraftData.Ingredient(TechType.PrecursorIonPowerCell, 1)
                    }
                },
                workstation = CraftTree.Type.Workbench,
                craftTime = 240F,

                onUsed = (Vehicle vehicle, int slotId, float unknown1, float unknown2) => OnUpgradeUsed(vehicle, slotId, 25, 0.15F),
                energyCost = 25,
                cooldown = 15,
                maxCharge = 25
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

        public static void RegisterSeamothUpgradeLevel(SeamothDefenseUpgradeLevel level)
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
                .SetVehicleUpgradeModule(EquipmentType.SeamothModule, QuickSlotType.Selectable)
                .WithOnModuleAdded(level.onAdded)
                .WithOnModuleRemoved(level.onRemoved)
                .WithCooldown(level.cooldown)
                .WithMaxCharge(level.maxCharge)
                .WithEnergyCost(level.energyCost)
                .WithOnModuleUsed(level.onUsed);

            prefabInfos.Add(info);
            prefab.Register();
        }

        public static void OnUpgradeUsed(Vehicle vehicle, int slotId, float radius, float force)
        {
            Collider[] collidersNearby = Physics.OverlapSphere(vehicle.transform.position, radius);
            Debug.Log($"Detected {collidersNearby.Length} colliders in a {radius} radius.");

            foreach (Collider collider in collidersNearby)
            {
                if (collider.transform.root == vehicle.transform || collider.transform == vehicle.transform) continue;

                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody == null) continue;

                Creature creature = collider.GetComponent<Creature>();
                if (creature == null) continue;

                Vector3 direction = vehicle.transform.position.DirectionTowards(collider.transform.position);
                float finalForce = Mathf.Lerp(force, 0, vehicle.transform.position.Distance(collider.transform.position) / radius) * rigidbody.mass;
                rigidbody.AddForce(direction * finalForce, ForceMode.VelocityChange);
            }
        }
    }
}
