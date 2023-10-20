using Nautilus.Assets;
using Nautilus.Handlers;
using System;
using UnityEngine;

namespace VS.Subnautica.QuestSystem.Behaviour.Quests
{
    public class QuestGatherResources : Quest
    {
        public int maxItemCount;
        protected int currentItemCount;
        public int CurrentItemCount { get { return currentItemCount; } }

        public TechType itemType;

        private static void Log(string msg) => QuestSystemPlugin.Log.LogInfo($"[QuestGatherResources]: {msg}");

        protected virtual void HandleItemAdded(InventoryItem item)
        {
            if (item.techType != itemType) return;

            currentItemCount++;
            Log($"Increasing current count from {currentItemCount - 1} to {currentItemCount} (on {maxItemCount}).");
            if (currentItemCount >= maxItemCount)
            {
                Log($"Completing quest...");
                CompleteQuest();
            }

            OnQuestUpdate();
        }

        #region Constructors

        public virtual QuestGatherResources WithMaxItemCount(int maxItemCount)
        {
            this.maxItemCount = maxItemCount;
            return this;
        }

        public virtual QuestGatherResources WithItemType(TechType itemType)
        {
            this.itemType = itemType;

            return this;
        }

        public new QuestGatherResources WithOnQuestComplete(Action onQuestComplete)
        {
            this.onQuestComplete = onQuestComplete;
            return this;
        }

        public new QuestGatherResources WithOnQuestFailed(Action onQuestFailed)
        {
            this.onQuestFailed = onQuestFailed;
            return this;
        }

        public virtual QuestGatherResources WithOnItemAddedHook(Action<InventoryItem> onItemAdded)
        {
            QuestHookHandler.onItemAdded += onItemAdded;
            return this;
        }

        public new QuestGatherResources Register()
        {
            QuestHookHandler.onItemAdded += HandleItemAdded;
            QuestRegistery.RegisterQuest(this);

            return this;
        }

        public static new QuestGatherResources Create(string name, string description)
        {
            QuestGatherResources quest = new QuestGatherResources()
            {
                name = name,
                description = description
            };

            return quest;
        }

        #endregion
    }

    public static class QuestGatherResourcesExtensions
    {
        public static QuestGatherResources AsGatherResourcesQuest(this Quest quest)
        {
            return quest as QuestGatherResources;
        }
    }
}
