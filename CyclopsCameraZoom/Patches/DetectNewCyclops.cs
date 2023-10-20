using HarmonyLib;

namespace VS.Subnautica.CyclopsCameraZoom.Patches
{
    using Utility;

    [HarmonyPatch(typeof(CyclopsEntryHatch))]
    internal class DetectNewCyclops
    {
        // When a new cyclop spawns, registering it.
        [HarmonyPatch(nameof(CyclopsEntryHatch.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(CyclopsEntryHatch __instance)
        {
            SubRoot subRoot = __instance.gameObject.FindAncestor<SubRoot>();
            if (subRoot == null) return;

            CyclopsRegistery.RegisterCyclops(subRoot);
        }
    }
}
