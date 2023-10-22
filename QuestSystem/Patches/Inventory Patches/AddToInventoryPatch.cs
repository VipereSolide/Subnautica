using UnityEngine;
using HarmonyLib;

using VS.Subnautica.QuestSystem.Behaviour;
using UWE;

namespace VS.Subnautica.QuestSystem.Patches.Inventory_Patches
{
    [HarmonyPatch(typeof(Inventory))]
    internal class AddToInventoryPatch
    {
        private static void Log(string msg) => QuestSystemPlugin.Log.LogInfo($"[AddToInventoryPatch]: {msg}");

        [HarmonyPatch(nameof(Inventory.OnAddItem))]
        [HarmonyPostfix]
        public static void OnAddItem_Postfix(Inventory __instance, InventoryItem item)
        {
            QuestHookHandler.OnItemAdded(item);
        }
    }
}
