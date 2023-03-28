using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public abstract class Destructible : Entity
    {
        #region Properties

        [SerializeField] private bool m_indestructible;
        public bool IsIndestructible => m_indestructible;


        [SerializeField] private int m_hitpoints;
        public int Hitpoints => m_hitpoints;

        private int m_currentHitpoints;
        public int CurrentHitpoints => m_currentHitpoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_currentHitpoints = m_hitpoints;
        }

        #endregion

        #region Public API

        public void ApplyDamage(int damage)
        {
            if (m_indestructible) return;

            m_currentHitpoints -= damage;

            if (m_currentHitpoints <= 0) OnDeath();
        }

        #endregion

        protected virtual void OnDeath()
        {
            Debug.Log(Score);
            Debug.Log(score);

            Player.Instance.AddGold(Score);

            _EventOnDeath?.Invoke();

            Destroy(gameObject);
        }

        private static HashSet<Enemy> allDestructibles;

        public static IReadOnlyCollection<Enemy> AllDestructibles => allDestructibles;

        protected virtual void OnEnable()
        {
            if(allDestructibles == null) allDestructibles = new HashSet<Enemy>();

            allDestructibles.Add((Enemy)this);
        }
        protected virtual void OnDestroy()
        {
            allDestructibles.Remove((Enemy)this);
        }

        public const int TeamIDNeutral = 0, TeamIDAlly = 1, TeamIDEnemy = 2;

        [SerializeField] private int teamID;
        public int TeamID => teamID;

        [SerializeField] private UnityEvent _EventOnDeath;
        public UnityEvent EventOnDeath => _EventOnDeath;

        #region Score
        [SerializeField] private int score;
        [HideInInspector] public int Score;
        #endregion
        public virtual void Use(EnemyAsset asset)
        {
            m_hitpoints = asset.hitpoints;
            score = asset.score;
        }
    }
}

