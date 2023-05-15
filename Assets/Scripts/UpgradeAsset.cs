using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class UpgradeAsset: ScriptableObject
    {
        [Header("������� ���")]
        public Sprite sprite;

        [Header("������� ���������")]
        public int[] costByLevel;
    }
}