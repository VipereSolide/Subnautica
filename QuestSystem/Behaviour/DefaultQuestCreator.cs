using System.Collections.Generic;
using UnityEngine;

using VS.Subnautica.QuestSystem.Behaviour.Quests;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public static class DefaultQuestCreator
    {
        public static void RegisterAllDefaultQuests()
        {
            // Resource Quests.
            QuestGatherResources
                .Create("Scavenger", QuestRegistery.GetGatherResourcesQuestDescription(TechType.ScrapMetal, 4))
                .WithMaxItemCount(4)
                .WithItemType(TechType.ScrapMetal)
                .Register();

            QuestGatherResources
                .Create("Titanium Madman", QuestRegistery.GetGatherResourcesQuestDescription(TechType.Titanium, 8))
                .WithMaxItemCount(8)
                .WithItemType(TechType.Titanium)
                .Register();

            QuestGatherResources
                .Create("My Precious", QuestRegistery.GetGatherResourcesQuestDescription(TechType.Gold, 4))
                .WithMaxItemCount(4)
                .WithItemType(TechType.Gold)
                .Register();

            QuestGatherResources
                .Create("Shiny Titanium?", QuestRegistery.GetGatherResourcesQuestDescription(TechType.Silver, 4))
                .WithMaxItemCount(4)
                .WithItemType(TechType.Silver)
                .Register();

            QuestGatherResources
                .Create("Heavy Workload", QuestRegistery.GetGatherResourcesQuestDescription(TechType.Lead, 3))
                .WithMaxItemCount(3)
                .WithItemType(TechType.Lead)
                .Register();

            QuestGatherResources
                .Create("True Electrician", QuestRegistery.GetGatherResourcesQuestDescription(TechType.CopperWire, 4))
                .WithMaxItemCount(4)
                .WithItemType(TechType.CopperWire)
                .Register();

            QuestGatherResources
                .Create("Copper Madman", QuestRegistery.GetGatherResourcesQuestDescription(TechType.Copper, 8))
                .WithMaxItemCount(8)
                .WithItemType(TechType.Copper)
                .Register();
        }
    }
}
