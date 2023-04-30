using System;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }
    public class LevelController : MonoSingleton<LevelController>
    {
        [SerializeField] protected float requiredTime;
        public float RequiredTime => requiredTime;

        [SerializeField] private UnityEvent EventLevelCompleted;

        private ILevelCondition condition;

        private bool IsLevelCompleted;

        private float levelTime;
        public float LevelTime => levelTime;

        protected void Start()
        {
            condition = GetComponentInChildren<ILevelCondition>();

            FindObjectOfType<EnemyWaveManager>().OnAllWavesFinished += CheckLevelConditions;
        }
        private void Update()
        {
            if (IsLevelCompleted == false)
            {
                levelTime += Time.deltaTime;
            }
        }
        private void CheckLevelConditions()
        {

            if (condition == null /*| condition.Length == 0*/) return;

            int numCompleted = 0;

            //foreach(var v in condition)
            //{
                //if(IsCompleted) numCompleted++;
            //}

            if (numCompleted /*== condition.Length*/ == 0)
            {
                Debug.Log("VSE LEVEL KONCHILSA");
                IsLevelCompleted = true;
                EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

        internal void EndLevel()
        {
            Debug.Log("Endlevel");
        }
    }
}