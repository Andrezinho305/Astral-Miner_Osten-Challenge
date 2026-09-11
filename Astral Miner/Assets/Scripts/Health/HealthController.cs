using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth;

    public int RemainingHealth
    {
        get 
        {
            return currentHealth / maxHealth;
        }
    }

    public bool isInvincible { get; set; }

    public UnityEvent OnDeath;
    public UnityEvent OnDamage;

    public void TakeDamage(int damage)
    {
        if (currentHealth == 0) { return; }

        if (isInvincible) {return; }

        currentHealth -= damage;

        if (currentHealth < 0) { currentHealth = 0; }

        if (currentHealth == 0) { OnDeath.Invoke(); }
        else {  OnDamage.Invoke(); }
    }

    public void AddHealth(int heal)
    {
        if (currentHealth == maxHealth) { return; }

        currentHealth += heal;

        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
    }



}
