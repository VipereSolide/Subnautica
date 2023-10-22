using System.Collections.Generic;

namespace VS.Subnautica.CyclopsCameraZoom.Utility
{
    /// <summary>
    /// Responsible for storing and referencing cyclops.
    /// </summary>
    public static class CyclopsRegistery
    {
        /// <summary>
        /// All registered cyclops.
        /// </summary>
        private static readonly List<CyclopsComponentManager> registeredCyclops = new List<CyclopsComponentManager>();
        /// <summary>
        /// Used to check if a sub root is registered without looping through
        /// every element in the Cyclops list.
        /// </summary>
        private static readonly List<SubRoot> subRootsChecklist = new List<SubRoot>();

        /// <summary>
        /// All registered cyclops.
        /// </summary>
        public static CyclopsComponentManager[] RegisteredCyclops() => registeredCyclops.ToArray();

        /// <summary>
        /// Searches for a cyclop with the same subroot in the regeistered cyclops list.
        /// </summary>
        /// <param name="subRoot">What sub root you want to get the cyclop scripts of.</param>
        /// <returns>A CyclopsComponentManager if the given subRoot is registered. Null otherwise.</returns>
        public static CyclopsComponentManager GetCyclopsBySubRoot(SubRoot subRoot)
        {
            foreach (var cyclop in registeredCyclops)
                if (cyclop.subroot == subRoot)
                    return cyclop;

            return null;
        }

        /// <summary>
        /// Checks whether a given subroot has been registered as a cyclops or not.
        /// </summary>
        /// <param name="subRoot">What subroot you want to know about.</param>
        /// <returns>True if the sub root is registered, false otherwise.</returns>
        public static bool IsRegistered(SubRoot subRoot)
        {
            return subRootsChecklist.Contains(subRoot);
        }

        /// <summary>
        /// Registers a new subroot in the registered cyclops list. Be aware that
        /// you can also register bases as cyclops, and you will have to run your
        /// own test to know whether the subroot is actually a cyclops or not.
        /// </summary>
        /// <param name="subRoot">What subroot you want to register.</param>
        /// <returns>True if it was successfully registered. False otherwise.</returns>
        public static bool RegisterCyclops(SubRoot subRoot)
        {
            if (IsRegistered(subRoot))
            {
                CyclopsCameraZoomPlugin.Log.LogError($"[CyclopsRegistery]: Cannot register already registered sub root!");
                return false;
            }

            subRootsChecklist.Add(subRoot);
            CyclopsComponentManager componentManager = CyclopsComponentManager
                .FromSubRoot(subRoot)
                .WithExternalCameras(subRoot.GetComponentInChildren<CyclopsExternalCams>())
                .WithEntryHatch(subRoot.GetComponentInChildren<CyclopsEntryHatch>());

            registeredCyclops.Add(componentManager);
            return true;
        }
    }
}
