using HarmonyLib;

namespace VipereSolide.Subnautica.MessingAroundMod.Patches.Vehicles.SeamothPatches
{
    [HarmonyPatch(typeof(SeamothStorageContainer))]
    internal class SeaMothStorageContainerPatch
    {
        [HarmonyPatch(nameof(SeamothStorageContainer.Awake))]
        [HarmonyPostfix]
        public static void PatchAwake_Postfix(SeamothStorageContainer __instance)
        {
            __instance.container?.Resize(4, 8);
        }
    }
}
