using UnityEngine;

namespace TowerDefense
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField]
        private string m_nickname;
        public string Nickname => m_nickname;
    }
}

