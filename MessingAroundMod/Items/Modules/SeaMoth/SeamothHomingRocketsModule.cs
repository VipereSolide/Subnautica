namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules.SeamothModules
{
    using Nautilus.Assets.PrefabTemplates;
    using Nautilus.Assets;
    using Nautilus.Crafting;
    using System.Collections.Generic;
    using System;
    using UnityEngine;
    using Utility.Vehicles;
    using VipereSolide.Subnautica.MessingAroundMod.Utility.Math;
    using Nautilus.Assets.Gadgets;
    using VipereSolide.Subnautica.MessingAroundMod.MonoBehaviours.Vehicles.Seamoth;

    public class SeamothHomingRocketsModule
    {
        public class SeamothHomingRocketsUpgradeLevel : UpgradeLevel { }

        public static List<SeamothHomingRocketsUpgradeLevel> Levels = new List<SeamothHomingRocketsUpgradeLevel>()
        {
            new SeamothHomingRocketsUpgradeLevel()
            {
                id = "SeamothHomingRocketsModuleMK1",
                display = "Seamoth Homing Rockets Module MK.1",
                description = string.Empty,
                unlockedAtStart = true,

                iconTechType = TechType.SeamothTorpedoModule,
                graphicsTechType = TechType.SeamothTorpedoModule,

                recipe = new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.Titanium),
                    }
                },
                workstation = CraftTree.Type.SeamothUpgrades,
                craftTime = 5F,

                onUsed = (Vehicle vehicle, int slotId, float unknown1, float unknown2) => OnUpgradeUsed(vehicle, slotId, 10, 5),
                energyCost = 0,
                cooldown = 0,
                maxCharge = 0
            },
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

        public static void RegisterSeamothUpgradeLevel(SeamothHomingRocketsUpgradeLevel level)
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

        private static Creature GetTarget(Vehicle vehicle, float maxDist)
        {
            GameObject[] creatures = GameObject.FindGameObjectsWithTag("Creature");

            Transform latestTransform = null;
            float closestDistance = float.MaxValue;
            float latestDotProduct = -1;

            Vector3 forward = Player.main.transform.forward;

            foreach (GameObject creature in creatures)
            {
                float distance = vehicle.transform.position.Distance(creature.transform.position);
                Vector3 directionTowardsCreature = vehicle.transform.position.DirectionTowards(creature.transform.position);
                float dotProduct = Vector3.Dot(forward, directionTowardsCreature);

                if (latestDotProduct < dotProduct)
                {
                    latestTransform = creature.transform;
                    closestDistance = distance;
                    latestDotProduct = dotProduct;
                }
            }

            Debug.Log(latestDotProduct);

            if (latestTransform == null) return null;
            return latestTransform.GetComponent<Creature>();
        }

        public static void OnUpgradeUsed(Vehicle vehicle, int slotId, int rocketsAmount, float radius)
        {
            const float offsetY = 2.25F;

            Creature targetCreature = GetTarget(vehicle, 100F);
            if (targetCreature == null) return;

            Vector3[] rocketsPositions = vehicle.transform.position.GetPointsInCircle3D(radius, rocketsAmount);

            foreach (Vector3 rocketPosition in rocketsPositions)
            {
                Vector3 position = rocketPosition - Vector3.up * offsetY;

                // Setups rocket's position, scale, and removes the collider since we
                // don't need it for now.
                GameObject rocketGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                rocketGameObject.transform.position = position;
                rocketGameObject.transform.localScale = Vector3.one * 0.5F;
                // UnityEngine.Object.Destroy(rocketGameObject.GetComponent<Collider>());

                SeamothHomingRocket rocket = rocketGameObject.AddComponent<SeamothHomingRocket>()
                    .Init(vehicle, targetCreature)
                    .WithSpeed(24F)
                    .WithRadius(2.5F)
                    .WithDamage(7F);
            }
        }
    }
}
