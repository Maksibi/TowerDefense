using TowerDefense;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float radius = 2.0f;

    [SerializeField] private Turret turret;
    private Enemy target;

    private Vector3 leadPosition;
    private Rigidbody2D selectedTargetRB;
    private Projectile projectile;

    private void Awake()
    {
        projectile = turret.TurretProperties.ProjectilePrefab;
    }
    private void Update()
    {
        if (target)
        {
            Vector2 targetVector = target.transform.position - transform.position;

            if (targetVector.magnitude <= radius)
            {
                CalculateLead();
                //Vector2 lp = leadPosition + transform.position;

                //float y = leadPosition.normalized.y - transform.position.normalized.y;
                //float x = leadPosition.normalized.x - transform.position.normalized.x;
                Vector2 dir = leadPosition - transform.position;
                float angle = ( Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
                //float angle = transform.eulerAngles.z - leadPosition.z;
                //float angle = Vector3.SignedAngle(transform.position, transform.position + leadPosition, Vector3.forward);

                //Vector3 rotationAngle = new Vector3(0.0f, 0.0f, angle);
                turret.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
                //turret.RotateTo(angle);

                Debug.Log(angle);
                Debug.DrawLine(transform.position, transform.position + (Vector3)targetVector, Color.blue);
                turret.Fire();
            }
            else
            {
                target = null;
            }
        }
        else
        {
            Collider2D enteredEntity = Physics2D.OverlapCircle(transform.position, radius);
            if (enteredEntity)
            {
                target = enteredEntity.transform.root.GetComponent<Enemy>();
            }
        }
    }
    private void CalculateLead()
    {
        selectedTargetRB = target.Rigidbody;

        leadPosition = Utils.MakeLead(projectile, target.transform, transform, selectedTargetRB);
        Debug.DrawLine(transform.position, leadPosition, Color.red);
        //Debug.Log("Lead   " + leadPosition);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void UseAsset(TowerAsset towerAsset)
    {
        if (turret != null)
        {
            turret.UseAsset(towerAsset.turretProperties);
        }
    }
}
