using UnityEngine;

public class ArenaBoundary : MonoBehaviour
{
    [SerializeField] private float radius = 20f;

    public float Radius => radius;
    public Vector3 Center => transform.position;

    private void OnDrawGizmosSelected()
    {
        int segments = 64;

        Vector3 previousPoint =
            transform.position + Vector3.right * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle =
                ((float)i / segments) * Mathf.PI * 2f;

            Vector3 newPoint =
                transform.position +
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    Mathf.Sin(angle) * radius,
                    0f
                );

            Gizmos.DrawLine(previousPoint, newPoint);

            previousPoint = newPoint;
        }
    }
}
