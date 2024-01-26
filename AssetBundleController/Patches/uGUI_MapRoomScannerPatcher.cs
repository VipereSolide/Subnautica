using HarmonyLib;

namespace Extensive.Patches
{
    [HarmonyPatch(typeof(uGUI_MapRoomScanner))]
    internal class uGUI_MapRoomScannerPatcher
    {
        [HarmonyPatch(nameof(uGUI_MapRoomScanner.UpdateAvailableTechTypes))]
        [HarmonyPrefix]
        public static bool UpdateAvailableTechTypes(uGUI_MapRoomScanner __instance)
        {
            __instance.availableTechTypes.Clear();
            ResourceTrackerDatabase.GetTechTypesInRange(__instance.mapRoom.transform.position, 2147483647, __instance.availableTechTypes);
            __instance.RebuildResourceList();

            return false;
        }
    }
}
