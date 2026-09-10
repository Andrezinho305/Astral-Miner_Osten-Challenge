using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyRotationSpeed;


    private Rigidbody2D _rb;
    private PlayerAwarenesController _playerAwarenesController;
    private Vector2 _targetDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAwarenesController = GetComponent<PlayerAwarenesController>();

    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateToTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if(_playerAwarenesController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenesController.DirectionToPlayer;
        }
        else 
        {
            _targetDirection = Vector2.zero; 
        }

    }

    private void RotateToTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward,_targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, enemyRotationSpeed * Time.fixedDeltaTime);

        _rb.SetRotation(rotation);

    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            _rb.linearVelocity = transform.up * enemySpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject); //destroi o inimigo

            //da dano ao jogador -- ou destroi o jogador

        }
    }
}
