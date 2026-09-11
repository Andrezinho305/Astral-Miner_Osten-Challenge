using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthController healthController;
    [SerializeField] private GameObject healthIconPrefab;
    [SerializeField] private Transform healthContainer;

    private readonly List<GameObject> healthIcons = new();

    private void Start()
    {
        UpdateHealthUI();

        healthController.OnHealthChange.AddListener(UpdateHealthUI);
    }

    private void OnDestroy()
    {
        healthController.OnHealthChange.RemoveListener(UpdateHealthUI);
    }

    private void UpdateHealthUI()
    {
        while (healthIcons.Count < healthController.currentHealth)
        {
            GameObject newIcon = Instantiate(healthIconPrefab, healthContainer);

            healthIcons.Add(newIcon);
        }

        while (healthIcons.Count > healthController.currentHealth)
        {
            int lastIndex = healthIcons.Count - 1;

            Destroy(healthIcons[lastIndex]);

            healthIcons.RemoveAt(lastIndex);
        }
    }
}

