﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VS.Subnautica.QuestSystem.Behaviour.Quests;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public static class DailyQuestHandler
    {
        public const int DAILY_QUEST_COUNT = 3;
        public static DailyQuestUI QuestUI;

        public static List<Quest> currentQuests = new List<Quest>();

        private static void Log(string msg) => QuestSystemPlugin.Log.LogInfo($"[DailyQuestHandler]: {msg}");

        public static void Init()
        {
            DayNightCycle.main.dayNightCycleChangedEvent.AddHandler(DayNightCycle.main.gameObject, (bool isDay) =>
            {
                if (isDay) UpdateDailyQuests();
            });
        }

        public static void UpdateDailyQuests()
        {
            if (QuestUI == null)
            {
                QuestUI = new GameObject().AddComponent<DailyQuestUI>();
                QuestUI.transform.SetParent(Player.main.transform);
                QuestUI.questText.text = "No Quest...";
            }

            Log("Updating quests...");

            if (currentQuests.Count == 0)
            {
                Log("No current quest found, adding new quests!");

                AddDailyQuests();
            }

            for (int i = 0; i < currentQuests.Count; i++)
            {
                if (currentQuests[i].isFinished)
                {
                    Log("Detected finished quest. Adding a new one instead.");
                    currentQuests[i] = GetRandomQuest();
                }
            }

            UpdateQuestText();
        }

        public static void AddDailyQuests()
        {
            for (int i = 0; i < DAILY_QUEST_COUNT; i++)
            {
                AddRandomQuest();
            }
        }

        public static void AddRandomQuest()
        {
            AddQuest(GetRandomQuest());
        }

        public static Quest GetRandomQuest()
        {
            Quest quest = QuestRegistery.RegisteredQuests[Random.Range(0, QuestRegistery.RegisteredQuests.Length)];
            Debug.Log(((QuestGatherResources)quest).CurrentItemCount);
            return quest;
        }

        public static void AddQuest(Quest quest)
        {
            Log($"New quest added: {quest.name} => {quest.description}");

            currentQuests.Add(quest);

            quest.onQuestComplete += () =>
                OnQuestComplete(currentQuests.Count - 1);

            quest.onQuestUpdate += () => UpdateQuestText();
        }

        public static void OnQuestComplete(int questIndex)
        {
            Quest completed = currentQuests[questIndex];
            Log($"Completed quest {completed.name} => {completed.description}!");

            UpdateDailyQuests();
        }

        public static void UpdateQuestText()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var quest in currentQuests)
            {
                if (quest.isFinished)
                    builder.Append("<s>");

                builder.AppendLine($"<b>{quest.name}</b>");

                string description = quest.description;
                if (quest.GetType() == typeof(QuestGatherResources))
                {
                    QuestGatherResources gatherResources = (QuestGatherResources)quest;
                    description += $" ({gatherResources.CurrentItemCount}/{gatherResources.maxItemCount})";
                }

                builder.AppendLine(description);

                if (quest.isFinished)
                    builder.Append("</s>");

                builder.AppendLine(" ");
            }

            QuestUI.questText.text = builder.ToString();
        }
    }
}
