using BepInEx.Logging;
using HarmonyLib;
using BepInEx;

namespace VipereSolide.Subnautica.MessingAroundMod
{
    using Items;
    using Items.Modules;
    using Items.Modules.SeamothModules;
    using VipereSolide.Subnautica.MessingAroundMod.Items.Modules.Prawn;

    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class MessingAroundModPlugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.viperesolide.subnautica.messingaroundmod";
        private const string PluginName = "Messing Around Mod";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);
        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
            Log = Logger;

            SeamothModulesManager.RegisterModules();
            ExosuitModulesManager.RegisterModules();
        }
    }
}
