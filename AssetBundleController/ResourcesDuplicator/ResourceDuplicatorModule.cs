using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Extensive.ResourcesDuplicator
{
    public class ResourceDuplicatorModule : MonoBehaviour
    {
        public PowerRelay powerRelay;

        public StorageContainer storage;
        public Constructable constructable;

        public static ResourceDuplicatorModule Create(GameObject gameObject, StorageContainer storage, Constructable constructable)
        {
            ResourceDuplicatorModule instance = gameObject.EnsureComponent<ResourceDuplicatorModule>();
            instance.constructable = constructable;
            instance.storage = storage;
            instance.ReferencePowerRelay();

            return instance;
        }

        public void ReferencePowerRelay()
        {
            powerRelay = GetComponentInParent<PowerRelay>();
            if (powerRelay == null) Debug.LogError("[ResourceDuplicatorMono]: Power relay is null!");
        }

        private void Start()
        {
            InvokeRepeating(nameof(DuplicateCall), 1, 30);
        }

        public void DuplicateCall() => StartCoroutine(DuplicateCoroutine());

        public IEnumerator DuplicateCoroutine()
        {
            if (storage.IsEmpty()) yield break;

            if (powerRelay == null) ReferencePowerRelay();
            if (powerRelay == null)
            {
                Debug.LogError("[ResourceDuplicatorMono]: Could not find any power relay in parent!");
                yield break;
            }

            // the duplicator shouldn't be able to run if it hasn't got
            // necessary power + a slight margin so your base doesn't
            // just run out of energy because of it.
            if (powerRelay.GetPower() <= 120F) yield break;

            // getting the resource's prefab to be able to duplicate it.
            TaskResult<GameObject> resource = new TaskResult<GameObject>();
            yield return StartCoroutine(GetResource(resource));
            GameObject resourcePrefab = resource.Get();

            // checking if the container can actually hold this resource.
            Pickupable pickupablePrefab = resourcePrefab.GetComponentInChildren<Pickupable>();
            if (pickupablePrefab == null || !storage.container.HasRoomFor(pickupablePrefab)) yield break;

            // duplicates and add the resource copy to the container.
            Transform spawnedPrefab = Instantiate(resourcePrefab).transform;
            spawnedPrefab.position = transform.position + Vector3.up * 2F;
            Pickupable pickupable = spawnedPrefab.GetComponentInChildren<Pickupable>();
            pickupable.Initialize();
            storage.container.UnsafeAdd(new InventoryItem(pickupable));

            // consumes 100 power units.
            powerRelay.ConsumeEnergy(100F, out _);
        }

        public IEnumerator GetResource(IOut<GameObject> result)
        {
            List<TechType> types = storage.container.GetItemTypes();
            foreach (TechType t in types)
            {
                CoroutineTask<GameObject> prefab = CraftData.GetPrefabForTechTypeAsync(t);
                yield return prefab;
                result.Set(prefab.GetResult());
                break;
            }
        }
    }
}
