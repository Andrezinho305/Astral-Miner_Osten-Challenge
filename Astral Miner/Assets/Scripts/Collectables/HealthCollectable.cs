using UnityEngine;

public class HealthCollectable : MonoBehaviour, ICollectableBehaivour
{
    [SerializeField] private int heal;
    public void OnCollect(GameObject player)
    {
        player.gameObject.GetComponentInParent<HealthController>().AddHealth(heal);
    }

}
