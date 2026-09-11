using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]private GameObject objectPrefab;

    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private float _timeUntilSpawn;


    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn-=Time.deltaTime;

        if(_timeUntilSpawn <= 0)
        {
            Instantiate(objectPrefab,transform.position,Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }




}
