using System;
using UnityEngine;

namespace TowerDefense
{
    public class Player : MonoSingleton<Player>
    {
        #region Editor Fields
        [SerializeField] private int _livesAmount;
        public int LivesAmount { get {return _livesAmount; } }

        public event Action<bool> OnPlayerDeath;

        [SerializeField] private SpaceShip ship;
        public SpaceShip ActiveShip => ship;

        [SerializeField] private GameObject _PlayerShipPrefab;

        [SerializeField] private int playerGold;
        #endregion
        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnLivesUpdate;

        public event Action OnPathEnd;

        private void Start()
        {
            OnGoldUpdate(playerGold);
            OnLivesUpdate(_livesAmount);
            Respawn();
        }
        protected override void Awake()
        {
            base.Awake();

            if (ship != null) Destroy(ship.gameObject);
        }
        public void TakeDamage(int damage)
        {
            _livesAmount -= damage;

            OnPathEnd?.Invoke();

            OnLivesUpdate(_livesAmount);

            if (_livesAmount <= 0)
            {
                _livesAmount = 0;
                OnPlayerDeath?.Invoke(false);
            }
        }
        public void AddGold(int gold)
        {
            playerGold += gold;
            OnGoldUpdate(playerGold);
        }
        public void SubtractGold(int gold)
        {
            playerGold -= gold;
            OnGoldUpdate(playerGold);
        }
        public void ChangeLives(int lives)
        {
            TakeDamage(lives);

            OnLivesUpdate(_livesAmount);
        }
        #region Private API
        /*private void OnShipDeath()
        {
            _livesAmount--;

            if (_livesAmount > 0)
            {
                Respawn();
            }
            else LevelSequenceController.Instance.RestartLevel();
        }*/
        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                if (ship != null)
                {
                    ship = newPlayerShip.GetComponent<SpaceShip>();
                    //ship.EventOnDeath.AddListener(OnShipDeath);
                }
            }
        }

        #endregion

        #region Score
        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        [SerializeField] private Tower towerPrefab;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            if (playerGold >= towerAsset.GoldCost & buildSite != null)
            {
                Tower tower = Instantiate(towerPrefab, buildSite.position, Quaternion.identity);

                tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.towerGraphics;
                tower.UseAsset(towerAsset);

                Destroy(buildSite.gameObject);

                SubtractGold(towerAsset.GoldCost);
            }
            else return;
        }
        #endregion
    }
}