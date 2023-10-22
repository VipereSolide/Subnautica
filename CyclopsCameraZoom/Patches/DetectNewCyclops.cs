using HarmonyLib;

namespace VS.Subnautica.CyclopsCameraZoom.Patches
{
    using Utility;

    [HarmonyPatch(typeof(CyclopsEntryHatch))]
    internal class DetectNewCyclops
    {
        // When a CyclopsEntryHatch's Start method is executed,
        // this means a new cyclops has spawned. We then try and
        // register this new cyclops by finding the SubRoot ancestor
        // from the hatch script. Could be really any other cyclops
        // component.
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
