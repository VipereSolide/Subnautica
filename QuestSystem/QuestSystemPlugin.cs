using BepInEx.Logging;
using BepInEx;

using HarmonyLib;

namespace VS.Subnautica.QuestSystem
{
    using Behaviour.Quests;
    using VS.Subnautica.QuestSystem.Behaviour;
    using VS.Subnautica.QuestSystem.Patches.Environment_Patches;

    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class QuestSystemPlugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.viperesolide.subnautica.questsystem";
        private const string PluginName = "Quest System";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);
        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
            Log = Logger;
        }

        private void Start()
        {
            DayNightCyclePatch.onAwake += () =>
            {
                DefaultQuestCreator.RegisterAllDefaultQuests();
                DailyQuestHandler.Init();
            };
        }
    }
}
