using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private SpaceShip enemyPrefab;
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
        }
        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        SpaceShip enemy = Instantiate(enemyPrefab, paths[pathIndex].StartArea.GetRandomInsideArea(), Quaternion.identity);
                        enemy.Use(asset);
                        enemy.GetComponent<AIController>().SetPath(paths[pathIndex]);
                    }
                }
                else Debug.LogWarning($"Invalid logindex in {name}");
            }

            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }
    }
}