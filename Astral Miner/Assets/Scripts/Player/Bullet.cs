using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damageDealt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(damageDealt); //da dano ao objeto -- ou destroi o jogador
            if(healthController.currentHealth<=0 ) { Destroy(collision.gameObject); }

            Destroy(gameObject);
        }

        if(collision.CompareTag("Asteroid"))
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(damageDealt); //da dano ao objeto -- ou destroi o jogador
            if (healthController.currentHealth <= 0) { Destroy(collision.gameObject); }

            Destroy(gameObject);
        }
    }





}
