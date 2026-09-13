using UnityEngine;

public class WorldUpgradeZone : MonoBehaviour
{
    [SerializeField] private ProgressionManager progressionManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        progressionManager.TryBuyWorldUpgrade();
    }
}
