using UnityEngine;

namespace TowerDefense
{
    public abstract class Spawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }
        #region Editor Fields
        [SerializeField] private CircleArea area;

        [SerializeField] private SpawnMode spawnMode;

        [SerializeField] private float spawnTime;

        [SerializeField] private int spawnCount;

        private bool _isActive;
        public bool IsActive { get => _isActive; set => _isActive = value; }
        #endregion

        protected abstract GameObject GenerateSpawnedEntity();

        private float timer;
        #region Unity Events
        private void Start()
        {
            _isActive = true;

            if (spawnMode == SpawnMode.Start) SpawnEntities();

            timer = spawnTime;
        }
        private void Update()
        {
            if (!_isActive) return;
            
            if (timer > 0) timer -= Time.deltaTime;

            if (spawnMode == SpawnMode.Loop & timer <= 0)
            {
                SpawnEntities();

                timer = spawnTime;
            }
        }
        private void SpawnEntities()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject entity = GenerateSpawnedEntity();

                entity.transform.position = area.GetRandomInsideArea();
            }
        }
        #endregion
    }
}