using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace com.rose.hc
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class UltraHardcore : BaseUnityPlugin
    {
        private const string MyGuid = "com.rose.hc";
        private const string PluginName = "Ultra Hardcore";
        private const string VersionString = "2.0_0.1.00";

        private static readonly Harmony Harmony = new Harmony(MyGuid);
        public static ManualLogSource Log;

        private void SendLoadedPluginMessage()
        {
            Logger.LogInfo($"Loaded {PluginName} {VersionString}");
        }

        private void Awake()
        {
            Harmony.PatchAll();
            SendLoadedPluginMessage();

            Log = Logger;
        }
    }
}