using UnityEngine;

public class PlayerAwarenesController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; } //faz com que outros scripts possam ver porém nao alterar a varaivel

    public Vector2 DirectionToPlayer {  get; private set; }

    [SerializeField] private float playerAwearenessDistance;

    private Transform _player;
    private PlayerRespawnManager _respawnManager;

    private void Start()
    {
        _respawnManager = FindFirstObjectByType<PlayerRespawnManager>();

        if (_respawnManager != null)
        {
            _respawnManager.OnPlayerSpawned.AddListener(SetPlayer);
        }

        FindCurrentPlayer();
    }

    private void OnDestroy()
    {
        if (_respawnManager != null)
        {
            _respawnManager.OnPlayerSpawned.RemoveListener(SetPlayer);
        }
    }


    void Update()
    {
        if (_player == null)
        {
            AwareOfPlayer = false;
            DirectionToPlayer = Vector2.zero;
            return;
        }

        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if(enemyToPlayerVector.magnitude <= playerAwearenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }

    private void SetPlayer(GameObject player)
    {
        _player = player.transform;
    }

    private void FindCurrentPlayer()
    {
        TwinStickMovement player = FindFirstObjectByType<TwinStickMovement>();

        if (player != null)
        {
            _player = player.transform;
        }
    }
}
