using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    [SerializeField] private Transform orbitCenter;
    [SerializeField] private float orbitRadius = 10f;
    [SerializeField] private float orbitSpeed = 30f;

    private float currentAngle;

    private void Start()
    {
        // Comeca em uma posicao aleatoria da orbita
        currentAngle = Random.Range(0f, 360f);

        UpdatePosition();
    }

    private void Update()
    {
        currentAngle += orbitSpeed * Time.deltaTime;

        if (currentAngle >= 360f)
        {
            currentAngle -= 360f;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f) * orbitRadius;

        transform.position = orbitCenter.position + offset;
    }

    private void OnDrawGizmosSelected()
    {
        if (orbitCenter == null)
            return;

        Gizmos.DrawWireSphere(orbitCenter.position, orbitRadius);
    }
}
