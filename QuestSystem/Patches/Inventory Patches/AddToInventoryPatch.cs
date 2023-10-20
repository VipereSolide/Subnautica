using UnityEngine;
using HarmonyLib;

using VS.Subnautica.QuestSystem.Behaviour;
using UWE;

namespace VS.Subnautica.QuestSystem.Patches.Inventory_Patches
{
    [HarmonyPatch(typeof(CraftData))]
    internal class AddToInventoryPatch
    {
        [HarmonyPatch(nameof(CraftData.AddToInventory))]
        [HarmonyPrefix]
        public static bool AddToInventory_Prefix(TechType techType, int num = 1, bool noMessage = false, bool spawnIfCantAdd = true)
        {
            TaskResult<GameObject> result = null;
            CoroutineHost.StartCoroutine(CraftData.AddToInventoryAsync(techType, result, num, noMessage, spawnIfCantAdd));

            if (result != null) QuestHookHandler.OnItemAdded(result.Get(), techType, num, noMessage, spawnIfCantAdd);

            return false;
        }
    }
}
