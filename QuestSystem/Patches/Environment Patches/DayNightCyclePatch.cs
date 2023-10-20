using HarmonyLib;
using System;

namespace VS.Subnautica.QuestSystem.Patches.Environment_Patches
{
    [HarmonyPatch(typeof(DayNightCycle))]
    public class DayNightCyclePatch
    {
        public static Action onAwake;

        [HarmonyPatch(nameof(DayNightCycle.Awake))]
        [HarmonyPostfix]
        public static void Awake_Postfix()
        {
            onAwake?.Invoke();
        }
    }
}
