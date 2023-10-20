using Nautilus.Assets;
using Nautilus.Handlers;
using System;
using UnityEngine;

namespace VS.Subnautica.QuestSystem.Behaviour.Quests
{
    public abstract class QuestGatherResources : Quest
    {
        public int maxItemCount;
        protected int currentItemCount;
        public int CurrentItemCount { get { return currentItemCount; } }

        public TechType itemType;

        protected virtual void HandleItemAdded(GameObject itemGameObject, TechType type, int amount, bool noMessage, bool spawnIfCant)
        {
            if (itemType == TechType.None)
            {
                return;
            }

            currentItemCount++;
            if (currentItemCount >= maxItemCount)
            {
                CompleteQuest();
                currentItemCount = 0;
            }
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
            QuestHookHandler.onItemAdded += HandleItemAdded;

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

        public virtual QuestGatherResources WithOnItemAddedHook(Action<GameObject, TechType, int, bool, bool> onItemAdded)
        {
            QuestHookHandler.onItemAdded += onItemAdded;
            return this;
        }

        public new QuestGatherResources Register()
        {
            QuestRegistery.RegisterQuest(this);
            return this;
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
