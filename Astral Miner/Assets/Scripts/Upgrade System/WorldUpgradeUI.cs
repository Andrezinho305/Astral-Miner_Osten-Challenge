using System.Collections;
using TMPro;
using UnityEngine;

public class WorldUpgradeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text feedbackText;

    private Coroutine feedbackCoroutine;


    public void UpdateUI(ProgressionManager progressionManager)
    {
        int purchased =
            progressionManager.UpgradesPurchasedThisCycle;

        int cost =
            progressionManager.WorldUpgradeCost;

        if (progressionManager.WorldUpgradeAvailable)
        {
            progressText.text = "PRONTO";
        }
        else
        {
            progressText.text = $"{purchased} / 7 REALIZADOS";
        }

        costText.text = $"$ {cost}";
    }

    public void ShowPurchaseFeedback(bool success)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        if (success)
        {
            feedbackText.text = "WORLD UPGRADE +1";
        }
        else
        {
            feedbackText.text = "UPGRADE INDISPONÍVEL";
        }

        feedbackCoroutine =
            StartCoroutine(HideFeedback());
    }

    private IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(1.5f);

        feedbackText.text = "";
    }
}
