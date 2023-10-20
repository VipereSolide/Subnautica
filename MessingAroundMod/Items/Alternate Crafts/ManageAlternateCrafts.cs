using Nautilus.Crafting;
using Nautilus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VipereSolide.Subnautica.MessingAroundMod.Utility;

namespace VipereSolide.Subnautica.MessingAroundMod.Items.Alternate_Crafts
{
    internal class ManageAlternateCrafts
    {
        public static void Register()
        {
            // ExosuitJetUpgradeModule
            CraftDataHandler.SetRecipeData
            (
                TechType.ExosuitJetUpgradeModule,
                new RecipeData()
                {
                    craftAmount = 1,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.Titanium, 4),
                        new CraftData.Ingredient(TechType.Lithium, 4),
                        new CraftData.Ingredient(TechType.Magnetite, 2),
                        new CraftData.Ingredient(TechTypeHelper.Ruby, 2),
                    }
                }
            );

            // ScrapMetal
            CraftDataHandler.SetRecipeData
            (
                TechType.ScrapMetal,
                new RecipeData()
                {
                    craftAmount = 0,
                    Ingredients = new List<CraftData.Ingredient>()
                    {
                        new CraftData.Ingredient(TechType.ScrapMetal, 1)
                    },
                    LinkedItems = Enumerable.Repeat(TechType.Titanium, 10).ToList()
                }
            );
        }
    }
}
