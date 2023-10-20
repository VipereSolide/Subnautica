using System;

using UnityEngine;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public static class QuestHookHandler
    {
        /// <summary>
        /// Called when any item enters player's inventory.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static Action<InventoryItem> onItemAdded;
        private static void Log(string msg) => QuestSystemPlugin.Log.LogInfo($"[QuestHookHandler]: {msg}");

        public static void OnItemAdded(InventoryItem item)
        {
            onItemAdded?.Invoke(item);
        }
    }
}
