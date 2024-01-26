using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace com.rose.hc
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class UltraHardcore : BaseUnityPlugin
    {
        private const string MyGuid = "com.rose.hc";
        private const string PluginName = "Long Knife Reach";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);
        public static ManualLogSource Log;

        private void SendLoadedPluginMessage()
        {
            Logger.LogInfo("Loaded {PluginName} (Version {VersionString}).");
        }

        private void Awake()
        {
            Harmony.PatchAll();
            SendLoadedPluginMessage();

            Log = Logger;
        }
    }
}