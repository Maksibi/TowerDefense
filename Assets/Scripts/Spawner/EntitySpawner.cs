using UnityEngine;

namespace SpaceShooter
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