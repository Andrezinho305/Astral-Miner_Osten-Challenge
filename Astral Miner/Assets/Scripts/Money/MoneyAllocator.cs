using UnityEngine;

public class MoneyAllocator : MonoBehaviour
{
    [SerializeField] private int _onKillMoney;

    private MoneyController _moneyController;
    private HealthController _healthController;


    private void Awake()
    {
        _moneyController = FindFirstObjectByType<MoneyController>();
        _healthController = GetComponent<HealthController>();
    }

    public void AllocateMoney()
    {
        _moneyController.AddMoney(_onKillMoney);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Asteroid"))
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();

            if (healthController.currentHealth <= 0)
            {
                AllocateMoney();
            }
        }
    }
}
