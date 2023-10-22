using HarmonyLib;
using UnityEngine;
using VS.Subnautica.Genes.Extensions;
using VS.Subnautica.Genes.GeneAttributes;
using static HangingStinger;

namespace VS.Subnautica.Genes.Patches
{
    [HarmonyPatch(typeof(Creature))]
    internal class CreaturePatcher
    {
        [HarmonyPatch(nameof(Creature.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(Creature __instance)
        {
            __instance.RegisterGenes(GeneAttributesProvider.GetAllGeneAttributesFromType(__instance.GetType()));
        }

        [HarmonyPatch(nameof(Creature.SetSize))]
        [HarmonyPrefix]
        public static bool SetSize_Prefix(Creature __instance)
        {
            return false;
        }
    }
}
