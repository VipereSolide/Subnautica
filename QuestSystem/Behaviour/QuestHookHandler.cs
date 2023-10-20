using System;

using UnityEngine;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public static class QuestHookHandler
    {
        /// <summary>
        /// GameObject itemGameObject, TechType type, int amount, bool noMessage, bool spawnIfCant
        /// </summary>
        /// <remarks>
        /// Called when any item is being crafted.
        /// </remarks>
        public static Action<GameObject, TechType, int, bool, bool> onItemAdded;

        public static void OnItemAdded(GameObject itemGameObject, TechType type, int amount, bool noMessage, bool spawnIfCant)
        {
            onItemAdded?.Invoke(itemGameObject, type, amount, noMessage, spawnIfCant);
        }
    }
}
