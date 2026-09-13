using UnityEngine;

public class PlayerUpgradeZone : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeType upgradeType;

    [SerializeField] private ProgressionManager progressionManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        progressionManager.TryBuyUpgrade(upgradeType);
    }
}
