using System.Collections.Generic;
using VS.Subnautica.QuestSystem.Behaviour.Quests;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public static class QuestRegistery
    {
        public static string GetNameForTechType(TechType item) =>
            item.AsString();

        public static string GetGatherResourcesQuestName(TechType item) =>
            $"Gather {GetNameForTechType(item)}.";

        public static string GetGatherResourcesQuestName(QuestGatherResources quest) =>
            GetGatherResourcesQuestName(quest.itemType);

        public static string GetGatherResourcesQuestDescription(TechType item, int amount) =>
            $"Collect {amount} {GetNameForTechType(item)}.";

        public static string GetGatherResourcesQuestDescription(QuestGatherResources quest) =>
            GetGatherResourcesQuestDescription(quest.itemType, quest.maxItemCount);

        private static readonly List<Quest> registeredQuests = new List<Quest>();

        public static Quest[] RegisteredQuests =>
            registeredQuests.ToArray();

        public static void RegisterQuest(Quest quest)
        {
            if (registeredQuests.Contains(quest))
                return;

            registeredQuests.Add(quest);
        }

        public static void UnregisterQuest(Quest quest)
        {
            if (registeredQuests.Contains(quest))
                registeredQuests.Remove(quest);
        }
    }
}
