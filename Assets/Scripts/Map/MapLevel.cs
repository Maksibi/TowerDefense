using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        public bool isCompleted { get { return resultPanel.gameObject.activeSelf & gameObject.activeSelf; } }

        private Episode _episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] images;

        public void LoadLevel()
        {
            if (_episode)
            {
                LevelSequenceController.Instance.StartEpisode(_episode);
            }
            else Debug.Log("TBC");
        }
        public void SetLevelData(Episode episode, int score)
        {
            _episode = episode;
            resultPanel.gameObject.SetActive(score > 0);
            if (images.Length == 3)
            {
                for (int i = 0; i < score; i++)
                {
                    images[i].color = Color.white;
                }
            }
        }
    }
}