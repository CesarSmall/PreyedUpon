using System.Linq;
using System;
using UnityEngine;

namespace games
{
    public class Spawner : MonoBehaviour
    {
        protected void SpawnObject(Transform Location, GameObject item)
        {
           Instantiate(item, Location);
        }

        protected void SpawnGroupObjects(Transform[] locations, GameObject[] items)
        {
            EnsureSameSizeArray(locations, items);

            for (int i = 0; i < items.Length; i++)
            {
                Instantiate(items[i], locations[i]);
            }
        }

        private Array EnsureSameSizeArray(Transform[] locations, GameObject[] items)
        {
            if (locations.Length == items.Length) return locations;

            return AdjustArrayLength(locations, items);
        }

        private Array AdjustArrayLength(Transform[] locations, GameObject[] items)
        {
            int difference = (items.Length - locations.Length);
            Transform[] newTransform = new Transform[difference];

            for (int i = 0; i < newTransform.Length; i++)
            {
                newTransform[i] = items[0].transform;
            }

            return locations.Concat(newTransform).ToArray();
        }
    }
}

