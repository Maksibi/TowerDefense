using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class UpgradeAsset: ScriptableObject
    {
        [Header("Внешний вид")]
        public Sprite sprite;

        [Header("Игровые параметры")]
        public int[] costByLevel;
    }
}