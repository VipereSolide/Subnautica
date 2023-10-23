using HarmonyLib;
using UnityEngine;

namespace VS.Subnautica.CyclopsHUDAlwaysOn.Patches
{
    [HarmonyPatch(typeof(CyclopsHelmHUDManager))]
    internal class CyclopsHelmHUDManagerPatcher
    {
        /// <summary>
        /// Makes sure the HUD gets enabled when the cyclops starts.
        /// </summary>
        [HarmonyPatch(nameof(CyclopsHelmHUDManager.Start))]
        [HarmonyPostfix]
        private static void Start_Postfix(CyclopsHelmHUDManager __instance)
        {
            __instance.hudActive = true;
            
            if (__instance.motorMode.engineOn)
            {
                __instance.engineToggleAnimator.SetTrigger("EngineOn");
                return;
            }

            __instance.engineToggleAnimator.SetTrigger("EngineOff");
        }

        /// <summary>
        /// Makes sure the HUD stays active when the user stops piloting it.
        /// </summary>
        [HarmonyPatch(nameof(CyclopsHelmHUDManager.StopPiloting))]
        [HarmonyPostfix]
        private static void StopPiloting_Postfix(CyclopsHelmHUDManager __instance)
        {
            __instance.hudActive = true;
            __instance.hornObject.SetActive(true);
        }
    }
}
