using UnityEngine;

public class MoneyAllocator : MonoBehaviour
{

    private MoneyController _moneyController;
    private HealthController _healthController;
    private ScalableEntity _scalableEntity;

    private void Awake()
    {
        _moneyController = FindFirstObjectByType<MoneyController>();

        _healthController = GetComponent<HealthController>();

        _scalableEntity = GetComponent<ScalableEntity>();
    }

    private void OnEnable()
    {
        if (_healthController != null)
        {
            _healthController.OnDeath.AddListener(AllocateMoney);
        }
    }

    private void OnDisable()
    {
        if (_healthController != null)
        {
            _healthController.OnDeath.RemoveListener(AllocateMoney);
        }
    }

    private void AllocateMoney()
    {
        if (_moneyController == null)
        {
            Debug.LogWarning("MoneyController não encontrado.");
            return;
        }

        if (_scalableEntity == null)
        {
            Debug.LogWarning(
                "WorldScalableEntity não encontrado em " + gameObject.name
            );

            return;
        }

        int reward = _scalableEntity.CurrentMoneyReward;

        _moneyController.AddMoney(reward);

        Debug.Log(
            $"{gameObject.name} morreu. Recompensa: ${reward}"
        );
        Debug.Log(
    "AllocateMoney executado | Reward: " +
    _scalableEntity.CurrentMoneyReward);
    }
}
