using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeZoneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text feedbackText;

    private Coroutine feedbackCoroutine;

    public void UpdateUI(
        PlayerUpgradeType upgradeType,
        ProgressionManager progressionManager)
    {
        int current =
            progressionManager.GetUpgradeCurrentLevel(upgradeType);

        int max =
            progressionManager.GetUpgradeMaxLevel(upgradeType);

        int cost =
            progressionManager.GetUpgradeCost(upgradeType);

        upgradeNameText.text =
            GetUpgradeName(upgradeType);

        progressText.text =
            $"{current} / {max}";

        if (current >= max)
        {
            costText.text = "MAX";
        }
        else
        {
            costText.text = $"$ {cost}";
        }
    }

    public void ShowPurchaseFeedback(
        PlayerUpgradeType upgradeType,
        bool success)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        if (success)
        {
            feedbackText.text =
                $"{GetUpgradeName(upgradeType)} +1";
        }
        else
        {
            feedbackText.text =
                "DINHEIRO INSUFICIENTE";
        }

        feedbackCoroutine =
            StartCoroutine(HideFeedback());
    }

    private IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(1.5f);

        feedbackText.text = "";
    }

    private string GetUpgradeName(
        PlayerUpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.BulletDamage:
                return "DANO";

            case PlayerUpgradeType.MaxHealth:
                return "VIDA MÁXIMA";

            case PlayerUpgradeType.MovementSpeed:
                return "VELOCIDADE";

            case PlayerUpgradeType.BulletSpeed:
                return "VELOCIDADE DA BALA";

            case PlayerUpgradeType.FireRate:
                return "CADÊNCIA DE TIRO";

            default:
                return upgradeType.ToString();
        }
    }
}