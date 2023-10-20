namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules.SeamothModules
{
    public static class SeamothModulesManager
    {
        public static void RegisterModules()
        {
            SeamothSpeedModules.Register();
            SeamothDefenseModule.Register();
            // SeamothHomingRocketsModule.Register();
        }
    }
}
