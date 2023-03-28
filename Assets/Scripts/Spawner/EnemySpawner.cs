using UnityEngine;

namespace TowerDefense
{
    public class EnemySpawner : EntitySpawner
    {
        [SerializeField] private SpaceShip enemyPrefab;
        [SerializeField] private Path path;
        [SerializeField] private EnemyAsset[] enemyAssets;

        protected override GameObject GenerateSpawnedEntity()
        {
            SpaceShip enemy =  Instantiate(enemyPrefab);

            enemy.Use(enemyAssets[Random.Range(0, enemyAssets.Length)]);

            enemy.GetComponent<AIController>().SetPath(path);

            return enemy.gameObject;
        }

    }
}