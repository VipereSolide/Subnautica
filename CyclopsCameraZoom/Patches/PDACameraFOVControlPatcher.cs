using HarmonyLib;

namespace VS.Subnautica.CyclopsCameraZoom.Patches
{
    using Utility;

    [HarmonyPatch(typeof(PDACameraFOVControl))]
    internal class PDACameraFOVControlPatcher
    {
        /// <summary>
        /// Checks whether the player is in a cyclops or not. If they
        /// are, then we want to stop the PDACameraFovControl from
        /// resetting the player FOV (otherwise, zooming in a cyclops
        /// would be reset all the time).
        /// </summary>
        /// <returns>
        /// True if we want the original update code to execute, false
        /// otherwise.
        /// </returns>
        [HarmonyPatch(nameof(PDACameraFOVControl.Update))]
        [HarmonyPrefix]
        public static bool Update_Prefix()
        {
            // Getting which sub the player is currently in, and
            // only executing logic if they are in one.
            SubRoot currentSubRoot = Player.main.currentSub;
            if (currentSubRoot == null) return true;

            // Now, base are heritating from SubRoot as well, so we
            // have to make sure we are in a Cyclops and not inside
            // a constructable. But no worries! In the
            // CyclopsExternalCamsPatcher, we built a dictionary
            // containing relations between cyclops and their cameras,
            // so we can simply check if the current sub is inside this
            // dictionary.
            bool isCurrentSubRootValid = CyclopsRegistery.IsRegistered(currentSubRoot);
            if (!isCurrentSubRootValid) return true;

            // Now that we're sure we're in a cyclops, we allow the original
            // update code to execute only if the player is not inside the
            // cameras of the cyclops (since the cameras only activate when
            // a player is inside them).
            return !CyclopsRegistery.GetCyclopsBySubRoot(currentSubRoot).externalCams.GetActive();
        }
    }
}