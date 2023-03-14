using UnityEngine;
using SpaceShooter;

public class Path : MonoBehaviour
{
    [SerializeField] private AIPointControl[] points;

    public int Lenght { get => points.Length; }
    public AIPointControl this[int i] { get => points[i]; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        AIPointControl previousPoint = null;

        foreach (AIPointControl point in points)
        {
            Gizmos.DrawSphere(point.transform.position, point.Radius);

            if (previousPoint != null) Gizmos.DrawLine(previousPoint.transform.position, point.transform.position);

            previousPoint = point;
        }
    }
}
