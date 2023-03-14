using SpaceShooter;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float radius = 2.0f;

    private Turret[] turrets;
    private Destructible target;

    private void Start()
    {
        turrets = GetComponentsInChildren<Turret>();
    }

    private void Update()
    {
        Collider2D enteredEntity = Physics2D.OverlapCircle(transform.position, radius);

        if (target)
        {
            Vector2 targetVector = target.transform.position - transform.position;

            if (targetVector.magnitude <= radius)
            {
                foreach (Turret turret in turrets)
                {
                    turret.transform.up = targetVector;
                    turret.Fire();
                }
            }
            else
            {
                target = null;
            }
        }
        else
        {
            if (enteredEntity)
            {
                target = enteredEntity.transform.root.GetComponent<Destructible>();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void SetTarget()
    {

    }
}
