using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        [Header("Visual")]
        public int GoldCost = 15;
        public Sprite GUISprite;
        public Sprite towerGraphics;

        [Header("Stats")]
        public float firerate = 0.5f;
        public TurretProperties turretProperties;
    }
}