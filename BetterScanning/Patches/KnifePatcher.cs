using HarmonyLib;

namespace VS.Subnautica.LongerKnifeReach.Patches
{
    [HarmonyPatch(typeof(Knife))]
    internal class KnifePatcher
    {
        public static float MAXIMUM_ATTACK_DISTANCE = 1.95F;

        [HarmonyPatch(nameof(Knife.OnToolUseAnim))]
        [HarmonyPrefix]
        public static void OnToolUseAnim_Prefix(Knife __instance)
        {
            __instance.attackDist = MAXIMUM_ATTACK_DISTANCE;
        }
    }
}
