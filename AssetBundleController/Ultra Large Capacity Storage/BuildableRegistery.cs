using static CraftData;

using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using Nautilus.Assets;

using UnityEngine;

namespace Extensive.Ultra_Large_Capacity_Storage
{
    public static class BuildableRegistery
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType
        (
            "UltraHighCapacityStorage",
            "Ultra High Capacity Storage",
            "Stores a very large amount of item."
        )
            .WithIcon(SpriteManager.Get(TechType.Locker));

        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab(Info);
            CloneTemplate cloneTemplate = new CloneTemplate(Info, "cd34fecd-794c-4a0c-8012-dd81b77f2840");

            cloneTemplate.ModifyPrefab += (obj) =>
            {
                ConstructableFlags constructableFlags = ConstructableFlags.Inside |
                                                        ConstructableFlags.Ground |
                                                        ConstructableFlags.Base |
                                                        ConstructableFlags.Submarine |
                                                        ConstructableFlags.Rotatable;

                GameObject model = obj.transform.Find("submarine_locker_04").gameObject;
                obj.transform.Find("submarine_locker_03_door_01").parent = model.transform;

                Constructable constructable = PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
                StorageContainer storageContainer = PrefabUtils.AddStorageContainer(obj, "StorageRoot", "UltraHighCapacityStorage", 8, 8, true);
            };

            customPrefab.SetGameObject(cloneTemplate);
            customPrefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.SetRecipe(new RecipeData(new Ingredient(TechType.PlasteelIngot, 2), new Ingredient(TechType.Lithium, 2), new Ingredient(TechType.Magnetite, 2)));
            customPrefab.Register();
        }
    }
}
