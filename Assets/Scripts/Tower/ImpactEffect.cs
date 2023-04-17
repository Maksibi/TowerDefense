using UnityEngine;

namespace TowerDefense
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float lifetime;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}