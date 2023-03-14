using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        #region Editor Fields
        [SerializeField] private TurretMode _turretMode;
        public TurretMode TurretMode => _turretMode;

        [SerializeField] private TurretProperties TurretProperties;
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
            if(TurretProperties == null) return;

            if (_refireTimer > 0 | CanFire == false) return;

            //if (spaceShip.DrawEnergy(TurretProperties.EnergyUsage) == false) return;
            //if (spaceShip.DrawAmmo(TurretProperties.AmmoUsage) == false) return;


            Projectile projectile = Instantiate(TurretProperties.ProjectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();

            _refireTimer = TurretProperties.Firerate;

            if(TurretProperties.LaunchSFX != null) AudioSource.PlayClipAtPoint(TurretProperties.LaunchSFX, transform.position);
        }
        public void AssignLoadout(TurretProperties props)
        {
            if (_turretMode != props.TurretMode) return;

            _refireTimer = 0;
            TurretProperties = props;
        }
        #endregion
    }
}
