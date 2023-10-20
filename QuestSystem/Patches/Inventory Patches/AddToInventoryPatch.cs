using UnityEngine;
using HarmonyLib;

using VS.Subnautica.QuestSystem.Behaviour;
using UWE;

namespace VS.Subnautica.QuestSystem.Patches.Inventory_Patches
{
    /*
    [HarmonyPatch(typeof(CraftData))]
    internal class AddToInventoryPatch
    {
        private static void Log(string msg) => QuestSystemPlugin.Log.LogInfo($"[AddToInventoryPatch]: {msg}");

        [HarmonyPatch(nameof(CraftData.AddToInventory))]
        [HarmonyPrefix]
        public static bool AddToInventory_Prefix(TechType techType, int num = 1, bool noMessage = false, bool spawnIfCantAdd = true)
        {
            TaskResult<GameObject> result = null;
            CoroutineHost.StartCoroutine(CraftData.AddToInventoryAsync(techType, result, num, noMessage, spawnIfCantAdd));

            if (result != null)
            {
                Log("Calling QuestHookHandler.OnItemAdded...");
                QuestHookHandler.OnItemAdded(result.Get(), techType, num, noMessage, spawnIfCantAdd);
            }
            else
            {
                Log("Result is null!");
            }

            return false;
        }
    }*/

    [HarmonyPatch(typeof(Inventory))]
    internal class AddToInventoryPatch
    {
        private static void Log(string msg) => QuestSystemPlugin.Log.LogInfo($"[AddToInventoryPatch]: {msg}");

        [HarmonyPatch(nameof(Inventory.OnAddItem))]
        [HarmonyPostfix]
        public static void OnAddItem_Postfix(InventoryItem item)
        {
            QuestHookHandler.OnItemAdded(item);
        }
    }
}
