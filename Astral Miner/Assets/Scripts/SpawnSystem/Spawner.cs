using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]private GameObject objectPrefab;

    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField]private int _maxObjects;

    private float _timeUntilSpawn;


    private List<GameObject> _spawnedObjects = new List<GameObject>();


    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if(_timeUntilSpawn <= 0)
        {
            RemoveDestroyedObjects();

            if (_spawnedObjects.Count < _maxObjects)
            {
                SpawnObject();
            }

            SetTimeUntilSpawn();
        }
    }

    private void SpawnObject()
    {
        GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

        _spawnedObjects.Add(newObject);
    }

    private void RemoveDestroyedObjects()
    {
        _spawnedObjects.RemoveAll(obj => obj == null);
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }




}
