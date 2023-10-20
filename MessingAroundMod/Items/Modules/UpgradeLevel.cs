using Nautilus.Crafting;
using System;

namespace VipereSolide.Subnautica.MessingAroundMod.Items.Modules
{
    public class UpgradeLevel
    {
        /// <summary>
        /// A unique name to distinguish your upgrade level from
        /// any other one.
        /// </summary>
        public string id;
        /// <summary>
        /// What name will be seen by the user anywhere the item
        /// is present (inventory, workstation, etc).
        /// </summary>
        public string display;
        /// <summary>
        /// A detailed description of what's your item used for.
        /// Even better yet is crusty small details like in the
        /// base game, where they detail the molecular formula.
        /// </summary>
        public string description;

        /// <summary>
        /// Whether the user has it unlocked the workstation from
        /// the very beginning of the game, or if they need to
        /// unlock it first.
        /// </summary>
        public bool unlockedAtStart;

        /// <summary>
        /// What tech type's icon will this item use.
        /// </summary>
        public TechType iconTechType;
        /// <summary>
        /// What tech type's model will this item use.
        /// </summary>
        public TechType graphicsTechType;

        /// <summary>
        /// How to craft this item.
        /// </summary>
        public RecipeData recipe;
        /// <summary>
        /// Where to craft this item.
        /// </summary>
        public CraftTree.Type workstation;
        /// <summary>
        /// How long does it take to craft this item.
        /// </summary>
        public float craftTime;

        /// <summary>
        /// On added onto any vehicle's modules. The int corresponds
        /// to the slot ID while the vehicle is what vehicle the
        /// module is attached to.
        /// </summary>
        public Action<Vehicle, int> onAdded;

        /// <summary>
        /// On removed from any vehicle's modules. The int corresponds
        /// to the slot ID while the vehicle is what vehicle the
        /// module is attached to.
        /// </summary>
        public Action<Vehicle, int> onRemoved;

        /// <summary>
        /// Called when user uses this module. The int is the slotId and
        /// the two floats are unknown.
        /// </summary>
        public Action<Vehicle, int, float, float> onUsed;

        public float energyCost;
        public float maxCharge;
        public float cooldown;
    }
}
