using System.Collections.Generic;

using UnityEngine;

namespace VS.Subnautica.CyclopsCameraZoom.Utility
{
    public static class CyclopsRegistery
    {
        private static readonly List<CyclopsComponentManager> CYCLOPS = new List<CyclopsComponentManager>();
        public static CyclopsComponentManager[] GetAllCyclops() => CYCLOPS.ToArray();

        private static readonly List<SubRoot> subRootRegistery = new List<SubRoot>();

        public static CyclopsComponentManager GetCyclopsBySubRoot(SubRoot subRoot)
        {
            foreach (var cyclop in CYCLOPS)
            {
                if (cyclop.subroot == subRoot) return cyclop;
            }

            return null;
        }

        public static bool IsRegistered(SubRoot subRoot)
        {
            return subRootRegistery.Contains(subRoot);
        }

        public static bool RegisterCyclops(SubRoot subRoot)
        {
            if (IsRegistered(subRoot))
            {
                return false;
            }

            subRootRegistery.Add(subRoot);

            CyclopsComponentManager componentManager = CyclopsComponentManager
                .FromSubRoot(subRoot)
                .WithExternalCameras(subRoot.GetComponentInChildren<CyclopsExternalCams>())
                .WithEntryHatch(subRoot.GetComponentInChildren<CyclopsEntryHatch>());

            CYCLOPS.Add(componentManager);

            return true;
        }
    }
}
