using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawnManager : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    [Header("Camera")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public UnityEvent<GameObject> OnPlayerSpawned;


    private GameObject currentPlayer;

    private void Start()
    {
        SpawnPlayer();
    }

    public void PlayerDied()
    {
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        currentPlayer = Instantiate( playerPrefab, spawnPoint.position, spawnPoint.rotation);

        cinemachineCamera.Follow = currentPlayer.transform;

        OnPlayerSpawned.Invoke(currentPlayer);
    }
}
