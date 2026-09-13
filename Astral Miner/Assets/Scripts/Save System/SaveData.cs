using System;

[Serializable]
public class SaveData
{
    // Indica se existe uma partida iniciada
    public bool hasSave;

    // =========================
    // PLAYER
    // =========================

    public float playerPositionX;
    public float playerPositionY;


    // =========================
    // MONEY
    // =========================

    public int money;


    // =========================
    // PROGRESSION
    // =========================

    public int currentCycle = 1;


    // =========================
    // UPGRADES DO CICLO ATUAL
    // =========================

    public int cycleDamageUpgrades;
    public int cycleHealthUpgrades;
    public int cycleMovementUpgrades;
    public int cycleBulletSpeedUpgrades;
    public int cycleFireRateUpgrades;


    // =========================
    // UPGRADES TOTAIS
    // =========================

    public int totalDamageUpgrades;
    public int totalHealthUpgrades;
    public int totalMovementUpgrades;
    public int totalBulletSpeedUpgrades;
    public int totalFireRateUpgrades;


    // =========================
    // WORLD
    // =========================

    public int worldLevel;
}
