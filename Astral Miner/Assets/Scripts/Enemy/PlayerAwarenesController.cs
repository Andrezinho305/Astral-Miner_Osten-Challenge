using UnityEngine;

public class PlayerAwarenesController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; } //faz com que outros scripts possam ver porém nao alterar a varaivel

    public Vector2 DirectionToPlayer {  get; private set; }

    [SerializeField] private float playerAwearenessDistance;

    private Transform _player;

    private void Awake()
    {
        _player = FindFirstObjectByType<TwinStickMovement>().transform;
    }


    void Update()
    {
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
}
