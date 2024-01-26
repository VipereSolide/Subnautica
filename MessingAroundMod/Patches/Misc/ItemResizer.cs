using HarmonyLib;

namespace VipereSolide.Subnautica.MessingAroundMod.Patches.Misc
{
    [HarmonyPatch(typeof(CraftData))]
    internal class ItemResizer
    {
        [HarmonyPatch(nameof(CraftData.GetItemSize))]
        [HarmonyPostfix]
        public static void Postfix(ref Vector2int __result)
        {
            __result = new Vector2int(1, 1);
        }
    }
}
