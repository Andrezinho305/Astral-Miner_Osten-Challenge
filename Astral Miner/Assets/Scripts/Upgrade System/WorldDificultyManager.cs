using UnityEngine;

public class WorldDificultyManager : MonoBehaviour
{
    [Header("World Level")]
    [SerializeField] private int worldLevel = 0;

    [Header("Enemy Scaling")]
    [SerializeField] private float enemyHealthMultiplier = 1.20f;
    [SerializeField] private float enemyMoneyMultiplier = 1.15f;

    [Header("Asteroid Scaling")]
    [SerializeField] private float asteroidHealthMultiplier = 1.20f;
    [SerializeField] private float asteroidMoneyMultiplier = 1.15f;

    public int WorldLevel => worldLevel;

    public void IncreaseWorldLevel()
    {
        worldLevel++;

        Debug.Log("World Level: " + worldLevel);
    }

    public int GetEnemyHealth(int baseHealth)
    {
        float finalHealth =
            baseHealth *
            Mathf.Pow(enemyHealthMultiplier, worldLevel);

        return Mathf.RoundToInt(finalHealth);
    }

    public int GetAsteroidHealth(int baseHealth)
    {
        float finalHealth =
            baseHealth *
            Mathf.Pow(asteroidHealthMultiplier, worldLevel);

        return Mathf.RoundToInt(finalHealth);
    }

    public int GetEnemyMoney(int baseMoney)
    {
        float finalMoney =
            baseMoney *
            Mathf.Pow(enemyMoneyMultiplier, worldLevel);

        return Mathf.RoundToInt(finalMoney);
    }

    public int GetAsteroidMoney(int baseMoney)
    {
        float finalMoney =
            baseMoney *
            Mathf.Pow(asteroidMoneyMultiplier, worldLevel);

        return Mathf.RoundToInt(finalMoney);
    }
}
