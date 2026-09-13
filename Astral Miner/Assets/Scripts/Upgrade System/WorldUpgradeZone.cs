using UnityEngine;

public class WorldUpgradeZone : MonoBehaviour
{
    [SerializeField] private ProgressionManager progressionManager;

    [SerializeField] private WorldUpgradeUI worldUpgradeUI;

    private void Start()
    {
        RefreshUI();

        progressionManager.OnProgressionChanged.AddListener(
            RefreshUI
        );
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        TwinStickMovement player =
            other.GetComponentInParent<TwinStickMovement>();

        if (player == null)
            return;

        bool success = progressionManager.TryBuyWorldUpgrade();

        worldUpgradeUI.ShowPurchaseFeedback(success);

        worldUpgradeUI.UpdateUI(progressionManager);
    }

    private void RefreshUI()
    {
        worldUpgradeUI.UpdateUI(
            progressionManager
        );
    }
}
