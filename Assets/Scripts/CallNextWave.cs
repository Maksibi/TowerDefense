using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class CallNextWave : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bonusText;

        private EnemyWaveManager _waveManager;

        private float timeToNextWave;

        private void Start()
        {
            _waveManager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;

            };
        }
        public void CallWave()
        {
            _waveManager.ForceNextWave();
        }
        private void Update()
        {
            int bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
            timeToNextWave -= Time.deltaTime;
            bonusText.text = (bonus).ToString();
        }
    }
}