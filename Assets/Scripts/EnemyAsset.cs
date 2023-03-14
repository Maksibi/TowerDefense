using UnityEngine;

namespace SpaceShooter
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
    }
}