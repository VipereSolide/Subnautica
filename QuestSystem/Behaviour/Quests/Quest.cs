using System;

namespace VS.Subnautica.QuestSystem.Behaviour.Quests
{
    public class Quest
    {
        public string name;
        public string description;

        protected Action onQuestComplete;
        protected Action onQuestFailed;

        #region Constructors

        public static Quest Create(string name, string description)
        {
            Quest quest = new Quest()
            {
                name = name,
                description = description
            };

            return quest;
        }

        public Quest WithOnQuestComplete(Action onQuestComplete)
        {
            this.onQuestComplete = onQuestComplete;
            return this;
        }

        public Quest WithOnQuestFailed(Action onQuestFailed)
        {
            this.onQuestFailed = onQuestFailed;
            return this;
        }

        public Quest Register()
        {
            QuestRegistery.RegisterQuest(this);
            return this;
        }

        #endregion

        public virtual void OnCompleteQuest()
        {
            onQuestComplete?.Invoke();
        }

        public virtual void OnFailQuest()
        {
            onQuestFailed?.Invoke();
        }

        public virtual void CompleteQuest()
        {
            OnCompleteQuest();
        }
    }
}
