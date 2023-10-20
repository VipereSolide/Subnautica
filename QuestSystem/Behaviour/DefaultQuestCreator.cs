using UnityEngine;

using VS.Subnautica.QuestSystem.Behaviour.Quests;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public static class DefaultQuestCreator
    {
        public static void RegisterAllDefaultQuests()
        {
            // Resource Gathering Quests.
            QuestGatherResources gatherScrapMetal = Quest
                .Create("Scavenger", QuestRegistery.GetGatherResourcesQuestDescription(TechType.ScrapMetal, 4))
                .AsGatherResourcesQuest()
                .WithMaxItemCount(4)
                .WithItemType(TechType.ScrapMetal)
                .WithOnQuestComplete(ScavengerOnComplete)
                .WithOnQuestFailed(ScavengerOnFailed)
                .WithOnItemAddedHook(ScavengerOnItemAdded)
                .Register();
        }

        public static void ScavengerOnComplete() { }
        public static void ScavengerOnFailed() { }
        public static void ScavengerOnItemAdded(GameObject itemGameObject, TechType type, int amount, bool noMessage, bool spawnIfCant) { }
    }
}
