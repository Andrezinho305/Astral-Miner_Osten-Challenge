using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private MoneyController _controller;
    private TMP_Text _currentMoney;

    private void Start()
    {
        _currentMoney = GetComponent<TMP_Text>();
        _controller = FindFirstObjectByType<MoneyController>();

        if (_controller != null)
        {
            _controller.OnMoneyChange.AddListener(UpdateScore);
            UpdateScore();
        }
        else
        {
            Debug.LogWarning("MoneyController não encontrado.");
        }
    }

    private void OnDestroy()
    {
        if (_controller != null)
        {
            _controller.OnMoneyChange.RemoveListener(UpdateScore);
        }
    }

    public void UpdateScore()
    {
        _currentMoney.text = $"$: {_controller.money}";
    }



}
