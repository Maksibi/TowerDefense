using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        [SerializeField] private List<EnemySpawner> spawners;

        public int levelScore = 3;

        private int _iterations = 0;

        private new void Start()
        {
            base.Start();

            Player.Instance.OnPlayerDeath += (value) =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResult(value);
            };
            requiredTime += Time.time;

            LevelSequenceController.Instance.OnLevelFinish += () =>
            {
                StopLevelActivity();
                MapsCompletion.SaveEpisodeResult(levelScore);

                if (requiredTime < Time.time)
                {
                    Debug.Log("Failed");
                    levelScore--;
                }
                ResultPanelController.Instance.ShowResult(true);
            };
            void LifeScoreChange(int _)
            {
                _iterations++;

                if (_iterations == 2)
                {
                    Debug.Log("Life failed");
                    levelScore--;
                    Player.OnLivesUpdate -= LifeScoreChange;
                }
            }
                
            Player.OnLivesUpdate += LifeScoreChange;
        }
        private void StopLevelActivity()
        {
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.enabled = false;
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
            }
            void DisableAll<T>() where T : MonoBehaviour
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