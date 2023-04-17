using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        private Episode _episode;
        [SerializeField] private TextMeshProUGUI text;

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
            text.text = $"{score}/3";
        }
    }
}