using UnityEngine;

public class Collectable : MonoBehaviour
{

    private ICollectableBehaivour _collectableBehaivour;

    private void Awake()
    {
        _collectableBehaivour = GetComponent<ICollectableBehaivour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        var player = collision.gameObject.GetComponentInParent<TwinStickMovement>();

        if (player != null )
        {
            _collectableBehaivour.OnCollect(player.gameObject);
            Destroy(gameObject);
        }
    }





}
