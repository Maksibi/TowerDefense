using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Lives
        }
        public UpdateSource source = UpdateSource.Gold;

        private TextMeshProUGUI text;

        void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            switch (source)
            {
                case UpdateSource.Gold:
                    Player.OnGoldUpdate += UpdateText;
                    break;

                case UpdateSource.Lives:
                    Player.OnLivesUpdate += UpdateText;
                    break;
            }
        }

        private void UpdateText(int value)
        {
           text.text = value.ToString();
        }
    }
}
