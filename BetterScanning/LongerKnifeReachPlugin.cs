using BepInEx.Logging;
using BepInEx;

using HarmonyLib;

namespace VS.Subnautica.LongerKnifeReach
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class LongerKnifeReachPlugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.vs.subnautica.longerknifereach";
        private const string PluginName = "Long Knife Reach";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);
        public static ManualLogSource Log;

        private void SendLoadedPluginMessage()
        {
            Logger.LogInfo("Successfully loaded {PluginName} (Version {VersionString})!");
            Logger.LogInfo("> Thank you very much for using my mod! - Vip");
        }

        private void Awake()
        {
            Harmony.PatchAll();
            SendLoadedPluginMessage();

            Log = Logger;
        }
    }
}