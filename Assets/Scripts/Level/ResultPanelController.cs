using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class ResultPanelController : MonoSingleton<ResultPanelController>
    {
        [SerializeField] private TextMeshProUGUI kills, score, time, result, nextButtonText;

        private bool IsSuccess;
 
        public void ShowResult(/*PlayerStatistics levelResults,*/ bool success)
        {
            gameObject.SetActive(true);
            IsSuccess = success;

            result.text = success ? "Win" : "Lose";
            nextButtonText.text = success ? "Next" : "Restart";
            result.color = success ? Color.green : Color.red;
            //kills.text = "Kills: " + levelResults.numKills.ToString();
            //score.text = "Score: " + levelResults.score.ToString();
            //time.text = "Time: " + levelResults.time.ToString();
        }
        private void UpdateCurrentLevelStats()
        {
            int timeBonus = (int)(LevelController.Instance.RequiredTime - LevelController.Instance.LevelTime);

            if(timeBonus > 0) { score.text += timeBonus; }
        }
        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);
           
            if (IsSuccess) LevelSequenceController.Instance.AdvanceLevel();

            else  SceneManager.LoadScene(1);
        }
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            Player.Instance.OnPlayerDeath += ShowResult;
            gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            Debug.Log("Disabled");
        }
    }
}