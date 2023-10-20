using System.Collections.Generic;

using UnityEngine;

namespace VS.Subnautica.CyclopsCameraZoom.Utility
{
    public static class CyclopsRegistery
    {
        private static readonly List<CyclopsComponentManager> CYCLOPS = new List<CyclopsComponentManager>();
        public static CyclopsComponentManager[] GetAllCyclops() => CYCLOPS.ToArray();

        public static CyclopsComponentManager GetCyclopBySubRoot(SubRoot subRoot)
        {
            foreach (var cyclop in CYCLOPS)
            {
                if (cyclop.subroot == subRoot) return cyclop;
            }

            Debug.LogError($"[CyclopsRegistery]: Could not find any registered cyclop with the subRoot \"{subRoot.transform.name}\"!");
            return null;
        }

        public static bool IsRegistered(SubRoot subRoot)
        {
            return GetCyclopBySubRoot(subRoot) != null;
        }

        public static bool RegisterCyclop(SubRoot subRoot)
        {
            if (IsRegistered(subRoot))
            {
                return false;
            }

            CyclopsComponentManager componentManager = CyclopsComponentManager
                .FromSubRoot(subRoot)
                .WithExternalCameras(subRoot.GetComponentInChildren<CyclopsExternalCams>())
                .WithEntryHatch(subRoot.GetComponentInChildren<CyclopsEntryHatch>());

            CYCLOPS.Add(componentManager);

            return true;
        }
    }
}
