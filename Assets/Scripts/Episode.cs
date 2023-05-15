using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        [SerializeField] private string episodeName;
        public string EpisodeName => episodeName;

        [SerializeField] private string[] levels;
        public string[] Levels => levels;

        [SerializeField] private Sprite previewImage;
        public Sprite PreviewImage => previewImage;
    }
}