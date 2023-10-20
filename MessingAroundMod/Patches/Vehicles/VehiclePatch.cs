using UnityEngine;
using HarmonyLib;

namespace VipereSolide.Subnautica.MessingAroundMod.Patches.Vehicles
{
    using Items.Modules.SeamothModules;

    [HarmonyPatch(typeof(Vehicle))]
    internal class VehiclePatch
    {
        [HarmonyPatch(nameof(Vehicle.ApplyPhysicsMove))]
        [HarmonyPrefix]
        public static void ApplyPhysicsMove_Prefix(Vehicle __instance, ref (float forwardForce, float backwardForce, float sidewardForce) __state)
        {
            __state.backwardForce = __instance.backwardForce;
            __state.forwardForce = __instance.forwardForce;
            __state.sidewardForce = __instance.sidewardForce;

            float multiplier = SeamothSpeedModules.GetSpeedMultiplier(__instance);
            __instance.backwardForce *= multiplier;
            __instance.forwardForce *= multiplier;
            __instance.sidewardForce *= multiplier;
        }

        [HarmonyPatch(nameof(Vehicle.ApplyPhysicsMove))]
        [HarmonyPostfix]
        public static void ApplyPhysicsMove_Postfix(Vehicle __instance, ref (float forwardForce, float backwardForce, float sidewardForce) __state)
        {
            __instance.backwardForce = __state.backwardForce;
            __instance.forwardForce = __state.forwardForce;
            __instance.sidewardForce = __state.sidewardForce;
        }
    }
}
