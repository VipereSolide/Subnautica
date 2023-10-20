using System.Collections.Generic;

using HarmonyLib;

namespace VS.Subnautica.CyclopsCameraZoom.Patches
{
    [HarmonyPatch(typeof(CyclopsExternalCams))]
    internal class CyclopsExternalCamsPatcher
    {
        public static readonly float ZOOMED_FOV = 30F;

        // Changing the FOV when user presses the jump key, in a camera.
        [HarmonyPatch(nameof(CyclopsExternalCams.HandleInput))]
        [HarmonyPostfix]
        public static void HandleInput_Postfix(CyclopsExternalCams __instance)
        {
            if (GameInput.GetKeyDown(CyclopsCameraZoomPlugin.config.toggle))
            {
                SNCameraRoot.main.SetFov(SNCameraRoot.main.CurrentFieldOfView == ZOOMED_FOV ? MiscSettings.fieldOfView : ZOOMED_FOV);
            }
        }

        // Resetting the field of view when changing of camera.
        [HarmonyPatch(nameof(CyclopsExternalCams.ChangeCamera))]
        [HarmonyPostfix]
        public static void ChangeCamera_Postfix()
        {
            SNCameraRoot.main.SyncFieldOfView();
        }
    }
}
