using HarmonyLib;
using System;

namespace VS.Subnautica.QuestSystem.Patches.Player_Patches
{
    [HarmonyPatch(typeof(Player))]
    internal class PlayerStartPatcher
    {
        public static Action onStart;

        [HarmonyPatch(nameof(Player.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix()
        {
            onStart?.Invoke();
        }
    }
}
