using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField] private float duration;

    private Invincibility _invincibilityController;

    private void Awake()
    {
        _invincibilityController = GetComponent<Invincibility>();
    }

    public void StartInvincibility()
    {
        _invincibilityController.StartInvincibility(duration);
    }




}
