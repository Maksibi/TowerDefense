using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] levels;

        void Start()
        {
            int drawLevel = 0;

            while(drawLevel < levels.Length & MapsCompletion.Instance.TryIndex(drawLevel, out Episode episode, out int score))
            {
                levels[drawLevel++].SetLevelData(episode, score);
                if (score == 0) break;
            }
            for (int i = drawLevel; i < levels.Length; i++)
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }
}