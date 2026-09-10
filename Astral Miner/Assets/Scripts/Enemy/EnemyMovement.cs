using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyRotationSpeed;


    private Rigidbody2D _rb;
    private PlayerAwarenesController _playerAwarenesController;
    private Vector2 _targetDirection;
    private float _changeDirectionTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAwarenesController = GetComponent<PlayerAwarenesController>();

        _targetDirection = Vector2.up;

    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateToTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();

        HandlePlayerTargeting();


    }

    private void RotateToTarget()
    {

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward,_targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, enemyRotationSpeed * Time.fixedDeltaTime);

        _rb.SetRotation(rotation);

    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionTimer -= Time.deltaTime;

        if (_changeDirectionTimer <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionTimer = Random.Range(1f, 5f);

        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwarenesController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenesController.DirectionToPlayer;
        }
    }



    private void SetVelocity()
    {
            _rb.linearVelocity = transform.up * enemySpeed;
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
