using UnityEngine;
using UnityEngine.Events;

public class MoneyController : MonoBehaviour
{
    public int money;

    public UnityEvent OnMoneyChange;

    public void AddMoney(int ammount)
    {
        money += ammount;
        OnMoneyChange.Invoke();
    }





}
