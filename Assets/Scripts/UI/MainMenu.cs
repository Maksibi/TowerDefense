using UnityEngine;
using UnityEngine.SceneManagement;
using static TowerDefense.Saver<int>;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject confirmationPanel;
        [SerializeField] private Button continueButton;
        private void Awake()
        {
            confirmationPanel.SetActive(false);
        }
        private void Start()
        {
            continueButton.interactable = FileHandler.CheckFile(MapsCompletion.FILENAME);
        }
        public void NewGame()
        {
            if (FileHandler.CheckFile(MapsCompletion.FILENAME)) confirmationPanel.SetActive(true);
            else
            {
                FileHandler.Reset(MapsCompletion.FILENAME);
                SceneManager.LoadScene(1);
            }
        }
        public void Continue()
        {
            SceneManager.LoadScene(1);
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void ResetGame()
        {
            FileHandler.Reset(MapsCompletion.FILENAME);
            SceneManager.LoadScene(1);
        }
    }
}