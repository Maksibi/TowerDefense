using UnityEngine;

namespace TowerDefense
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted = false;

        public bool IsCompleted { get { return isCompleted; }}

        //private EnemyWaveManager manager;
        private void Start()
        {
            //manager = FindObjectOfType<EnemyWaveManager>();

            /*manager*/FindObjectOfType<EnemyWaveManager>().OnAllWavesFinished += () => 
            {
                isCompleted = true;
            };
        }
        /*-private void OnDisable()
        {
            manager.OnAllWavesFinished -= () =>
            {
                isCompleted = false;
            };
        }*/
    }
} 