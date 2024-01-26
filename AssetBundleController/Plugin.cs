using BepInEx.Logging;
using HarmonyLib;
using BepInEx;

using Extensive.Ultra_Large_Capacity_Storage;
using Extensive.ResourcesDuplicator;
using Extensive.Patches;

namespace Extensive
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        private const string MyGuid = "com.vs.subnautica.extensive";
        private const string PluginName = "Extensive";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);
        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Log = Logger;

            // ResourceTrackerDatabaseExtension.ExtendResourceTypes();
            BuildableRegistery.Register();
            ResourcesDuplicatorRegistery.Register();
        }
    }
}
