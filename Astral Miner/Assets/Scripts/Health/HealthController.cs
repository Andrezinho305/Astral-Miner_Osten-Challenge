using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public bool isInvincible { get; set; }

    public UnityEvent OnDeath;
    public UnityEvent OnDamage;
    public UnityEvent OnHealthChange;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        currentHealth = maxHealth;

        OnHealthChange.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth == 0) { return; }

        if (isInvincible) {return; }

        currentHealth -= damage;

        OnHealthChange.Invoke();
        
        if (currentHealth < 0) { currentHealth = 0; }

        if (currentHealth == 0) { OnDeath.Invoke(); }
        else { OnDamage.Invoke(); }
    }

    public void AddHealth(int heal)
    {
        if (currentHealth == maxHealth) { return; }

        currentHealth += heal;

        OnHealthChange.Invoke();

        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;

        OnHealthChange.Invoke();
    }

}
