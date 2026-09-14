using UnityEngine;

public class PlayerUpgradeZone : MonoBehaviour
{
    [SerializeField]
    private PlayerUpgradeType upgradeType;

    [SerializeField] private ProgressionManager progressionManager;

    [SerializeField] private UpgradeZoneUI upgradeZoneUI;

    private void Start()
    {
        RefreshUI();

        progressionManager.OnProgressionChanged.AddListener(RefreshUI);
    }

    private void OnDestroy()
    {
        if (progressionManager != null)
        {
            progressionManager.OnProgressionChanged.RemoveListener(
                RefreshUI
            );
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TwinStickMovement player =
            collision.GetComponentInParent<TwinStickMovement>();

        if (player == null)
            return;

        Debug.Log("Player entrou na zona de upgrade");

        bool success =
           progressionManager.TryBuyUpgrade(upgradeType);

        upgradeZoneUI.ShowPurchaseFeedback(
            upgradeType,
            success
        );

        if (success)
        {
            SoundManager.Instance.PlayUpgrade();
        }

        upgradeZoneUI.UpdateUI(
            upgradeType,
            progressionManager
        );

    }

    private void RefreshUI()
    {
        upgradeZoneUI.UpdateUI(
            upgradeType,
            progressionManager
        );
    }

}
