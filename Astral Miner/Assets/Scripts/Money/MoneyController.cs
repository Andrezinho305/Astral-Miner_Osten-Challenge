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

    public bool TrySpendMoney(int amount)
    {
        if (money < amount)
        {
            return false;
        }

        money -= amount;
        OnMoneyChange.Invoke();

        return true;
    }

    public void RemoveMoneyOnDeath()
    {
        money = money / 2;
        OnMoneyChange.Invoke();
    }



}
