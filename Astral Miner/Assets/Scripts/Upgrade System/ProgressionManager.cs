using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnProgressionChanged;

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

    public int WorldUpgradeCost => CalculateWorldUpgradeCost();

    private GameObject currentPlayer;

    private void Awake()
    {
        // Escuta o spawn antes do PlayerRespawnManager criar a primeira nave.
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

        SaveProgress();

        OnProgressionChanged.Invoke();

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
            Debug.Log("Os 7 upgrades do ciclo ainda não foram comprados.");
            return false;
        }

        if (!moneyController.TrySpendMoney(WorldUpgradeCost))
        {
            Debug.Log("Dinheiro insuficiente para aumentar o nível do mundo.");
            return false;
        }

        AdvanceWorld();

        SaveProgress();

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

        OnProgressionChanged.Invoke();

        Debug.Log($"Novo ciclo iniciado: {CurrentCycle}");
    }

    public int GetUpgradeCurrentLevel(PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                return cycleDamageUpgrades;

            case PlayerUpgradeType.MaxHealth:
                return cycleHealthUpgrades;

            case PlayerUpgradeType.MovementSpeed:
                return cycleMovementUpgrades;

            case PlayerUpgradeType.BulletSpeed:
                return cycleBulletSpeedUpgrades;

            case PlayerUpgradeType.FireRate:
                return cycleFireRateUpgrades;
        }

        return 0;
    }

    public int GetUpgradeMaxLevel(PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                return 3;

            case PlayerUpgradeType.MaxHealth:
                return 1;

            case PlayerUpgradeType.MovementSpeed:
                return 1;

            case PlayerUpgradeType.BulletSpeed:
                return 1;

            case PlayerUpgradeType.FireRate:
                return 1;
        }

        return 0;
    }

    private int CalculateWorldUpgradeCost()
    {
        int totalCost = 0;

        // 3 upgrades de dano
        for (int i = 0; i < 3; i++)
        {
            int level = totalDamageUpgrades - cycleDamageUpgrades + i;

            totalCost += Mathf.RoundToInt(
                damageBaseCost *
                Mathf.Pow(costMultiplier, level)
            );
        }


        // 1 upgrade de vida
        {
            int level =
                totalHealthUpgrades -
                cycleHealthUpgrades;

            totalCost += Mathf.RoundToInt(
                healthBaseCost *
                Mathf.Pow(costMultiplier, level)
            );
        }


        // 1 upgrade de movimento
        {
            int level =
                totalMovementUpgrades -
                cycleMovementUpgrades;

            totalCost += Mathf.RoundToInt(
                movementSpeedBaseCost *
                Mathf.Pow(costMultiplier, level)
            );
        }


        // 1 upgrade de velocidade da bala
        {
            int level =
                totalBulletSpeedUpgrades -
                cycleBulletSpeedUpgrades;

            totalCost += Mathf.RoundToInt(
                bulletSpeedBaseCost *
                Mathf.Pow(costMultiplier, level)
            );
        }


        // 1 upgrade de fire rate
        {
            int level =
                totalFireRateUpgrades -
                cycleFireRateUpgrades;

            totalCost += Mathf.RoundToInt(
                fireRateBaseCost *
                Mathf.Pow(costMultiplier, level)
            );
        }


        return totalCost;
    }

    public void WriteSaveData(SaveData data)
    {
        data.currentCycle =
            CurrentCycle;

        // Ciclo atual
        data.cycleDamageUpgrades =
            cycleDamageUpgrades;

        data.cycleHealthUpgrades =
            cycleHealthUpgrades;

        data.cycleMovementUpgrades =
            cycleMovementUpgrades;

        data.cycleBulletSpeedUpgrades =
            cycleBulletSpeedUpgrades;

        data.cycleFireRateUpgrades =
            cycleFireRateUpgrades;


        // Totais
        data.totalDamageUpgrades =
            totalDamageUpgrades;

        data.totalHealthUpgrades =
            totalHealthUpgrades;

        data.totalMovementUpgrades =
            totalMovementUpgrades;

        data.totalBulletSpeedUpgrades =
            totalBulletSpeedUpgrades;

        data.totalFireRateUpgrades =
            totalFireRateUpgrades;
    }

    public void LoadSaveData(SaveData data)
    {
        CurrentCycle =
            data.currentCycle;

        // Ciclo atual
        cycleDamageUpgrades =
            data.cycleDamageUpgrades;

        cycleHealthUpgrades =
            data.cycleHealthUpgrades;

        cycleMovementUpgrades =
            data.cycleMovementUpgrades;

        cycleBulletSpeedUpgrades =
            data.cycleBulletSpeedUpgrades;

        cycleFireRateUpgrades =
            data.cycleFireRateUpgrades;


        // Totais
        totalDamageUpgrades =
            data.totalDamageUpgrades;

        totalHealthUpgrades =
            data.totalHealthUpgrades;

        totalMovementUpgrades =
            data.totalMovementUpgrades;

        totalBulletSpeedUpgrades =
            data.totalBulletSpeedUpgrades;

        totalFireRateUpgrades =
            data.totalFireRateUpgrades;


        ApplyAllPlayerUpgrades();

        OnProgressionChanged.Invoke();

        Debug.Log(
            "Progressão carregada. Ciclo: " +
            CurrentCycle
        );
    }

    // Permite que outros sistemas, como o menu de pausa, salvem o estado atual.
    public void SaveCurrentProgress()
    {
        SaveProgress();
    }

    private void SaveProgress()
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogWarning("SaveManager não encontrado.");
            return;
        }

        SaveData data = SaveManager.Instance.Data;

        if (data == null)
        {
            Debug.LogWarning("SaveData não encontrado.");
            return;
        }

        // Marca que já existe uma partida salva
        data.hasSave = true;

        // =========================
        // PROGRESSÃO
        // =========================

        WriteSaveData(data);


        // =========================
        // DINHEIRO
        // =========================

        if (moneyController != null)
        {
            data.money = moneyController.money;
        }


        // =========================
        // WORLD LEVEL
        // =========================

        if (worldDifficultyManager != null)
        {
            data.worldLevel =
                worldDifficultyManager.WorldLevel;
        }


        // =========================
        // POSIÇÃO DO PLAYER
        // =========================

        if (currentPlayer != null)
        {
            data.playerPositionX =
                currentPlayer.transform.position.x;

            data.playerPositionY =
                currentPlayer.transform.position.y;
        }


        // =========================
        // GRAVA O ARQUIVO
        // =========================

        SaveManager.Instance.WriteSaveFile();

        Debug.Log("Progresso salvo.");
    }

}
