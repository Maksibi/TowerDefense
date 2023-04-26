using UnityEngine;

namespace TowerDefense
{
    public class Turret : MonoBehaviour
    {
        #region Editor Fields
        [SerializeField] private TurretMode _turretMode;
        public TurretMode TurretMode => _turretMode;

        [SerializeField] private TurretProperties _turretProperties;
        public TurretProperties TurretProperties { set { _turretProperties = value; } get => _turretProperties; }
        #endregion
        private float _refireTimer;

        public bool CanFire => _refireTimer <= 0;

        #region UnityEvents
        private void Update()
        {
            if (_refireTimer >= 0) _refireTimer -= Time.deltaTime;

            else if (_turretMode == TurretMode.Auto) Fire();
        }
        public void Fire()
        {
            if(_turretProperties == null) return;

            if (_refireTimer > 0 | CanFire == false) return;

            //if (spaceShip.DrawEnergy(TurretProperties.EnergyUsage) == false) return;
            //if (spaceShip.DrawAmmo(TurretProperties.AmmoUsage) == false) return;


            Projectile projectile = Instantiate(_turretProperties.ProjectilePrefab, transform.position, transform.rotation);

            _refireTimer = _turretProperties.Firerate;

            if(_turretProperties.LaunchSFX != null) AudioSource.PlayClipAtPoint(_turretProperties.LaunchSFX, transform.position);
        }
        public void AssignLoadout(TurretProperties props)
        {
            if (_turretMode != props.TurretMode) return;

            _refireTimer = 0;
            _turretProperties = props;
        }
        public void UseAsset(TurretProperties turretProperties)
        {
            _turretProperties = turretProperties;
            TurretProperties = turretProperties;
        }
        public void RotateTo(float angle)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        #endregion
    }
}
