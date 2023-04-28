using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }
        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        public static Action<float> OnWavePrepare;

        [SerializeField] private PathGroup[] pathGroups;

        [SerializeField] private float prepareTime = 10f;

        public float GetRemainingTime() { return prepareTime - Time.time; }

        private event Action OnWaveReady;

        private void Awake()
        {
            enabled = false;
        }
        private void Update()
        {
            if (Time.time >= prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < pathGroups.Length; i++)
            {
                foreach (Squad squad in pathGroups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }
            }
        }
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepareTime);
            prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }
        [SerializeField] private EnemyWave nextWave;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (nextWave) nextWave.Prepare(spawnEnemies);
            return nextWave;
        }
    }
}
