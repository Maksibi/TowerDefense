using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        [SerializeField] private List<EnemySpawner> spawners;

        public int LevelScore => 1;

        private new void Start()
        {
            base.Start();

            Player.Instance.OnPlayerDeath += (value) =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResult(value);
            };
            LevelSequenceController.Instance.OnLevelFinish += () =>
            {
                StopLevelActivity();
                MapsCompletion.SaveEpisodeResult(LevelScore);
                ResultPanelController.Instance.ShowResult(true);
            };
        }
        private void StopLevelActivity()
        {
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.enabled = false;
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
            }
            void DisableAll<T>() where T: MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
        }
    }
}