using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 8f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction =
                ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;

            rb.linearVelocity = direction * knockbackForce;
        }
    }
}
