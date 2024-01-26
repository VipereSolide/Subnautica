using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using Nautilus.Assets;

using static CraftData;

using UnityEngine;
using System.CodeDom;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.IO;

namespace Extensive.ResourcesDuplicator
{
    public static class ResourcesDuplicatorRegistery
    {
        public static readonly string CLASS_ID = "ResourcesDuplicator";
        public static readonly string DISPLAY_NAME = "Resource Duplicator";
        public static readonly string DESCRIPTION = "Analyzes the chemical structure of any given resource and creates an exact copy every 30 seconds at the cost of very high energy (100u).";
        public static readonly string MODEL_IDENTIFIER = "5be64297-0340-4d4b-adce-0841d2b25483";

        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType(CLASS_ID, DISPLAY_NAME, DESCRIPTION).WithIcon(SpriteManager.Get(TechType.Workbench));

        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab(Info);
            CloneTemplate cloneTemplate = new CloneTemplate(Info, MODEL_IDENTIFIER);
            cloneTemplate.ModifyPrefab += ModifyPrefab;

            customPrefab.SetGameObject(cloneTemplate);
            customPrefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium)));
            customPrefab.Register();
        }

        public static void ModifyPrefab(GameObject gameObject)
        {
            GameObject.Destroy(gameObject.GetComponent<Workbench>());
            GameObject.Destroy(gameObject.GetComponent<CrafterLogic>());
            GameObject.Destroy(gameObject.GetComponent<CrafterGhostModel>());

            GameObject model = gameObject.transform.Find("model").gameObject;
            SkinnedMeshRenderer skinnedMeshRenderer = model.transform.Find("submarine_Workbench/workbench_geo").GetComponent<SkinnedMeshRenderer>();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Assets/Textures/resource_duplicator_albedo.png");
            skinnedMeshRenderer.material.mainTexture = ImageUtils.LoadTextureFromFile(path);
            skinnedMeshRenderer.material.SetFloat("_SpecInt", 0.1F);

            ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Ground | ConstructableFlags.Base | ConstructableFlags.Submarine | ConstructableFlags.Rotatable;

            Constructable constructable = PrefabUtils.AddConstructable(gameObject, Info.TechType, constructableFlags, model);
            StorageContainer storage = PrefabUtils.AddStorageContainer(gameObject, "ResourcesDuplicatorStorageRoot", "ResourcesDuplicator", 4, 6, true);
            ResourceDuplicatorModule.Create(gameObject, storage, constructable);
        }
    }
}
