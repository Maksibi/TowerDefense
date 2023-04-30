using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private SpaceShip enemyPrefab;
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;

        private int activeEnemyCount = 0;

        public event Action OnAllWavesFinished;

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
            Player.Instance.OnPathEnd += RecordEnemyDeath;
        }
        #region Methods
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
                        activeEnemyCount++;
                        enemy.OnLifeEnd += RecordEnemyDeath;
                    }
                }
                else Debug.LogWarning($"Invalid logindex in {name}");
            }
            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }
        public void ForceNextWave()
        {
            if (currentWave)
            {
                Player.Instance.AddGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else if (activeEnemyCount == 0)
            {
                OnAllWavesFinished?.Invoke();
            }
        }
        private void RecordEnemyDeath()
        {
            Debug.Log("MY LIFE BE LIKE");
            if (--activeEnemyCount <= 0)
            {
                ForceNextWave();
            }
        }
        #endregion
    }
}