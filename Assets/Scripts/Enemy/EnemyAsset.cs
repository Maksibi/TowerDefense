using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header ("Visual")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animations;

        [Header("Stats")]
        public float movespeed = 1.0f;
        public int hitpoints = 10;
        public int score = 1;
        public int damage = 1;
    }
}