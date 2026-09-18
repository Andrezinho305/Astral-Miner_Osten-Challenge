using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float lifetime = 30f;
    private ICollectableBehaivour _collectableBehaivour;

    private void Awake()
    {
        _collectableBehaivour = GetComponent<ICollectableBehaivour>();
    }

    private void Start()
    {
        Destroy(gameObject,lifetime);
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
