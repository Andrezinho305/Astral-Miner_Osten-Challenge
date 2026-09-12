using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField] private float _chanceToDrop;

    private CollectableSpawner _spawner;

    private void Awake()
    {
        _spawner = FindFirstObjectByType<CollectableSpawner>();
    }

    public void RandomlyDropCollectable()
    {
        float random = Random.Range(0f, 1f);

        if (_chanceToDrop >= random)
        {
            _spawner.SpawnCollectable(transform.position);
        }
    }
}
