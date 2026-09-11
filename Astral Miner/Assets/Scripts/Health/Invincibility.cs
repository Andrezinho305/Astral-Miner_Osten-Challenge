using System.Collections;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();
    }

    public void StartInvincibility(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        healthController.isInvincible = true;
        yield return new WaitForSeconds(duration);
        healthController.isInvincible = false;
    }

}
