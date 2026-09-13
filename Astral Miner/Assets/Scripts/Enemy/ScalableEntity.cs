using UnityEngine;

public enum WorldEntityType
{
    Enemy,
    Asteroid
}

public class ScalableEntity : MonoBehaviour
{
    [SerializeField]
    private WorldEntityType entityType;

    [Header("Base Values")]
    [SerializeField] private int baseHealth = 3;
    [SerializeField] private int baseMoneyReward = 10;

    private WorldDificultyManager worldDifficultyManager;
    private HealthController healthController;

    public int CurrentMoneyReward { get; private set; }

    private void Awake()
    {
        worldDifficultyManager =
            FindFirstObjectByType<WorldDificultyManager>();

        healthController =
            GetComponent<HealthController>();

        ApplyWorldScaling();
    }

    private void ApplyWorldScaling()
    {
        if (worldDifficultyManager == null)
        {
            Debug.LogWarning("WorldDifficultyManager não encontrado.");
            return;
        }

        if (healthController == null)
        {
            Debug.LogWarning("HealthController não encontrado.");
            return;
        }

        int finalHealth;
        int finalMoney;

        if (entityType == WorldEntityType.Enemy)
        {
            finalHealth =
                worldDifficultyManager.GetEnemyHealth(baseHealth);

            finalMoney =
                worldDifficultyManager.GetEnemyMoney(baseMoneyReward);
        }
        else
        {
            finalHealth =
                worldDifficultyManager.GetAsteroidHealth(baseHealth);

            finalMoney =
                worldDifficultyManager.GetAsteroidMoney(baseMoneyReward);
        }

        healthController.SetMaxHealth(finalHealth);

        CurrentMoneyReward = finalMoney;
    }
}
