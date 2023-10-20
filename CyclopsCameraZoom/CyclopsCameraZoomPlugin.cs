using System.Text;

using BepInEx.Logging;
using BepInEx;

using Nautilus.Handlers;
using HarmonyLib;

namespace VS.Subnautica.CyclopsCameraZoom
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class CyclopsCameraZoomPlugin : BaseUnityPlugin
    {
        // https://github.com/RamuneNeptune/SubnauticaMods.Nautilus/blob/main/SubnauticaMods/PrawnSuitLightSwitch/Config.cs
        public static Config config { get; } = OptionsPanelHandler.RegisterModOptions<Config>();

        private const string MyGuid = "com.vs.subnautica.cyclopscamerazoom";
        private const string PluginName = "Cyclops Camera Zoom";
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