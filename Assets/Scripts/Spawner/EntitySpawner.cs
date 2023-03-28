using UnityEngine;

namespace TowerDefense
{
    public class EntitySpawner : Spawner
    {
        private GameObject[] entityPrefabs;

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate (entityPrefabs[Random.Range(0, entityPrefabs.Length)]);
        }
    }
}