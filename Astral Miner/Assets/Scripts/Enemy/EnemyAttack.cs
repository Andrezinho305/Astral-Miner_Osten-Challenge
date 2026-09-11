using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damageDealt;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var healthController = collision.gameObject.GetComponentInParent<HealthController>();
            healthController.TakeDamage(damageDealt); //da dano ao objeto -- ou destroi o jogador

            Destroy(gameObject); //destroi o inimigo após a colisão
        }
    }

}
