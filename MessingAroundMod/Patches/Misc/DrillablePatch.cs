using UnityEngine;
using HarmonyLib;

namespace VipereSolide.Subnautica.MessingAroundMod.Patches.Misc
{
    using Items.Modules.Prawn;
    using Utility.Vehicles;
    using Vehicles.Prawn;

    [HarmonyPatch(typeof(Drillable))]
    internal class DrillablePatch
    {
        private static float GetTotalHealth(Drillable drillable)
        {
            float totalHealth = 0f;

            for (int i = 0; i < drillable.health.Length; i++)
            {
                totalHealth += drillable.health[i];
            }

            return totalHealth;
        }

        [HarmonyPatch(nameof(Drillable.OnDrill))]
        [HarmonyPrefix]
        public static bool OnDrill_Prefix(Drillable __instance, Vector3 position, Exosuit exo, out GameObject hitObject)
        {
            float totalHealth = GetTotalHealth(__instance);
            __instance.drillingExo = exo;

            int closestMeshIndex = __instance.FindClosestMesh(position, out Vector3 center);
            hitObject = __instance.renderers[closestMeshIndex].gameObject;

            __instance.timeLastDrilled = Time.time;

            if (totalHealth > 0f)
            {
                float previousClosestMeshHealth = __instance.health[closestMeshIndex];
                __instance.health[closestMeshIndex] = Mathf.Max(0f, __instance.health[closestMeshIndex] - PrawnDrillArmPatch.GetDamage(exo));

                totalHealth -= previousClosestMeshHealth - __instance.health[closestMeshIndex];

                if (previousClosestMeshHealth > 0f && __instance.health[closestMeshIndex] <= 0f)
                {
                    __instance.renderers[closestMeshIndex].gameObject.SetActive(false);
                    __instance.SpawnFX(__instance.breakFX, Vector3.zero);

                    if (__instance.resources.Length != 0)
                    {
                        __instance.StartCoroutine(__instance.SpawnLootAsync(Vector3.zero));
                    }
                }

                if (totalHealth <= 0f)
                {
                    __instance.SpawnFX(__instance.breakAllFX, Vector3.zero);

                    if (__instance.deleteWhenDrilled)
                    {
                        ResourceTracker component = __instance.GetComponent<ResourceTracker>();

                        if (component)
                        {
                            component.OnBreakResource();
                        }

                        float time = __instance.lootPinataOnSpawn ? 6f : 0f;
                        __instance.Invoke("DestroySelf", time);
                    }
                }
            }

            BehaviourUpdateUtils.Register(__instance);

            return false;
        }

        private static void __instance_onDrilled(Drillable drillable)
        {
            throw new System.NotImplementedException();
        }
    }
}
