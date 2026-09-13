using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MoneyController moneyController;
    [SerializeField] private PlayerRespawnManager respawnManager;

    [Header("Base Costs")]
    [SerializeField] private int damageBaseCost = 100;
    [SerializeField] private int healthBaseCost = 250;
    [SerializeField] private int movementSpeedBaseCost = 250;
    [SerializeField] private int bulletSpeedBaseCost = 200;
    [SerializeField] private int fireRateBaseCost = 300;

    [Header("Cost Scaling")]
    [SerializeField] private float costMultiplier = 1.25f;

    [Header("Player Base Stats")]
    [SerializeField] private int baseBulletDamage = 1;
    [SerializeField] private int baseHealth = 3;
    [SerializeField] private float baseMovementSpeed = 5f;
    [SerializeField] private float baseBulletSpeed = 10f;
    [SerializeField] private float baseTimeBetweenShots = 0.5f;

    [Header("Player Upgrade Values")]
    [SerializeField] private int damageIncrease = 1;
    [SerializeField] private int healthIncrease = 1;
    [SerializeField] private float movementSpeedIncrease = 0.5f;
    [SerializeField] private float bulletSpeedIncrease = 1f;
    [SerializeField] private float fireRateMultiplier = 0.9f;

    [SerializeField] private WorldDificultyManager worldDifficultyManager;

    // Ciclo atual
    public int CurrentCycle { get; private set; } = 1;

    // Compras realizadas apenas neste ciclo
    private int cycleDamageUpgrades;
    private int cycleHealthUpgrades;
    private int cycleMovementUpgrades;
    private int cycleBulletSpeedUpgrades;
    private int cycleFireRateUpgrades;

    // Upgrades acumulados durante toda a partida
    private int totalDamageUpgrades;
    private int totalHealthUpgrades;
    private int totalMovementUpgrades;
    private int totalBulletSpeedUpgrades;
    private int totalFireRateUpgrades;

    // Soma de tudo que foi gasto nos upgrades deste ciclo
    private int currentCycleUpgradeValue;

    public int UpgradesPurchasedThisCycle =>
        cycleDamageUpgrades +
        cycleHealthUpgrades +
        cycleMovementUpgrades +
        cycleBulletSpeedUpgrades +
        cycleFireRateUpgrades;

    public bool WorldUpgradeAvailable => UpgradesPurchasedThisCycle >= 7;

    public int WorldUpgradeCost => currentCycleUpgradeValue;

    private GameObject currentPlayer;

    private void Start()
    {
        if (respawnManager != null)
        {
            respawnManager.OnPlayerSpawned.AddListener(SetPlayer);
        }
    }

    private void OnDestroy()
    {
        if (respawnManager != null)
        {
            respawnManager.OnPlayerSpawned.RemoveListener(SetPlayer);
        }
    }

    private void SetPlayer(GameObject player)
    {
        currentPlayer = player;

        ApplyAllPlayerUpgrades();
    }

    public bool TryBuyUpgrade(PlayerUpgradeType upgradeType)
    {
        if (WorldUpgradeAvailable)
        {
            Debug.Log("Compre o World Upgrade para continuar.");
            return false;
        }

        if (!CanPurchaseUpgrade(upgradeType))
        {
            Debug.Log("Limite deste upgrade atingido neste ciclo.");
            return false;
        }

        int cost = GetUpgradeCost(upgradeType);

        if (!moneyController.TrySpendMoney(cost))
        {
            Debug.Log("Dinheiro insuficiente.");
            return false;
        }

        currentCycleUpgradeValue += cost;

        RegisterUpgrade(upgradeType);

        ApplyAllPlayerUpgrades();

        Debug.Log(
            $"Upgrade comprado: {upgradeType} | " + $"Custo: {cost} | " + $"Progresso: {UpgradesPurchasedThisCycle}/9");

        return true;
    }

    private bool CanPurchaseUpgrade(PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                return cycleDamageUpgrades < 3;

            case PlayerUpgradeType.MaxHealth:
                return cycleHealthUpgrades < 1;

            case PlayerUpgradeType.MovementSpeed:
                return cycleMovementUpgrades < 1;

            case PlayerUpgradeType.BulletSpeed:
                return cycleBulletSpeedUpgrades < 1;

            case PlayerUpgradeType.FireRate:
                return cycleFireRateUpgrades < 1;
        }

        return false;
    }

    private void RegisterUpgrade(PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                cycleDamageUpgrades++;
                totalDamageUpgrades++;
                break;

            case PlayerUpgradeType.MaxHealth:
                cycleHealthUpgrades++;
                totalHealthUpgrades++;
                break;

            case PlayerUpgradeType.MovementSpeed:
                cycleMovementUpgrades++;
                totalMovementUpgrades++;
                break;

            case PlayerUpgradeType.BulletSpeed:
                cycleBulletSpeedUpgrades++;
                totalBulletSpeedUpgrades++;
                break;

            case PlayerUpgradeType.FireRate:
                cycleFireRateUpgrades++;
                totalFireRateUpgrades++;
                break;
        }
    }

    public int GetUpgradeCost(PlayerUpgradeType upgradeType)
    {
        int baseCost = GetBaseCost(upgradeType);
        int totalLevel = GetTotalUpgradeLevel(upgradeType);

        float scaledCost =
            baseCost *
            Mathf.Pow(costMultiplier, totalLevel);

        return Mathf.RoundToInt(scaledCost);
    }

    private int GetBaseCost(PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                return damageBaseCost;

            case PlayerUpgradeType.MaxHealth:
                return healthBaseCost;

            case PlayerUpgradeType.MovementSpeed:
                return movementSpeedBaseCost;

            case PlayerUpgradeType.BulletSpeed:
                return bulletSpeedBaseCost;

            case PlayerUpgradeType.FireRate:
                return fireRateBaseCost;
        }

        return 0;
    }

    private int GetTotalUpgradeLevel(PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                return totalDamageUpgrades;

            case PlayerUpgradeType.MaxHealth:
                return totalHealthUpgrades;

            case PlayerUpgradeType.MovementSpeed:
                return totalMovementUpgrades;

            case PlayerUpgradeType.BulletSpeed:
                return totalBulletSpeedUpgrades;

            case PlayerUpgradeType.FireRate:
                return totalFireRateUpgrades;
        }

        return 0;
    }

    private void ApplyAllPlayerUpgrades()
    {
        if (currentPlayer == null)
            return;

        HealthController healthController =
            currentPlayer.GetComponent<HealthController>();

        TwinStickMovement movement =
            currentPlayer.GetComponent<TwinStickMovement>();

        PlayerShoot playerShoot =
            currentPlayer.GetComponent<PlayerShoot>();


        int finalDamage =
            baseBulletDamage +
            totalDamageUpgrades * damageIncrease;

        int finalHealth =
            baseHealth +
            totalHealthUpgrades * healthIncrease;

        float finalMovementSpeed =
            baseMovementSpeed +
            totalMovementUpgrades * movementSpeedIncrease;

        float finalBulletSpeed =
            baseBulletSpeed +
            totalBulletSpeedUpgrades * bulletSpeedIncrease;

        float finalTimeBetweenShots =
            baseTimeBetweenShots *
            Mathf.Pow(fireRateMultiplier, totalFireRateUpgrades);


        healthController.SetMaxHealth(finalHealth);

        movement.SetMovementSpeed(finalMovementSpeed);

        playerShoot.SetBulletDamage(finalDamage);
        playerShoot.SetBulletSpeed(finalBulletSpeed);
        playerShoot.SetTimeBetweenShots(finalTimeBetweenShots);
    }

    public bool TryBuyWorldUpgrade()
    {
        if (!WorldUpgradeAvailable)
        {
            Debug.Log("Os 9 upgrades do ciclo ainda não foram comprados.");
            return false;
        }

        if (!moneyController.TrySpendMoney(WorldUpgradeCost))
        {
            Debug.Log("Dinheiro insuficiente para aumentar o nível do mundo.");
            return false;
        }

        AdvanceWorld();

        return true;
    }

    private void AdvanceWorld()
    {
        CurrentCycle++;

        cycleDamageUpgrades = 0;
        cycleHealthUpgrades = 0;
        cycleMovementUpgrades = 0;
        cycleBulletSpeedUpgrades = 0;
        cycleFireRateUpgrades = 0;

        currentCycleUpgradeValue = 0;

        worldDifficultyManager.IncreaseWorldLevel();

        Debug.Log($"Novo ciclo iniciado: {CurrentCycle}");
    }
}
