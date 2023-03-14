using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpawner : EntitySpawner
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Path path;
        [SerializeField] private EnemyAsset[] enemyAssets;

        protected override GameObject GenerateSpawnedEntity()
        {
            Enemy enemy =  Instantiate(enemyPrefab);

            enemy.Use(enemyAssets[Random.Range(0, enemyAssets.Length)]);

            enemy.GetComponent<AIController>().SetPath(path);

            return enemy.gameObject;
        }

    }
}