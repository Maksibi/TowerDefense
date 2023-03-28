using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        #region Editor Fields
        [SerializeField] private float velocity, lifetime;
        public float Velocity => velocity;

        [SerializeField] private int damage;

        [SerializeField] private ImpactEffect impactEffect;
        #endregion

        private float timer;
        #region unity events
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > lifetime) Destroy(gameObject);


            float stepLength = Time.deltaTime * velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hitRay = Physics2D.Raycast(transform.position, transform.up, stepLength);
            if (hitRay)
            {
                Enemy enemy = hitRay.collider.transform.root.GetComponent<Enemy>();

                if (enemy != null & enemy != _parent)
                {
                    enemy.ApplyDamage(damage);

                    if (_parent == null) OnProjectileLifeEnd(hitRay.collider, hitRay.point);//////

                    if (enemy.CurrentHitpoints <= 0)
                    {
                        if (enemy.TeamID == 2) Player.Instance.AddKill();
                    }
                }
                OnProjectileLifeEnd(hitRay.collider, hitRay.point);
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }
        #endregion
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            if (impactEffect != null) Instantiate(impactEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        private Enemy _parent;

        public void SetParentShooter(Enemy parent)
        {
            _parent = parent;
        }
    }
}