using UnityEngine;
using HarmonyLib;

namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules.ScannerRoom
{
    [HarmonyPatch(typeof(MapRoomFunctionality))]
    internal class MapRoomFunctionalityPatch
    {
        [HarmonyPatch(nameof(MapRoomFunctionality.Start))]
        [HarmonyPrefix]
        public static void Start_Prefix(MapRoomFunctionality __instance)
        {
            __instance.storageContainer.Resize(4, 4);
        }
    }
}
