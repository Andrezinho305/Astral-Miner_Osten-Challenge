using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootOffset;


    [Header("Values")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots; //fire speed

    private PlayerControls _controls;

    private float _lastFireTime;
    private bool _isShooting;
    private float _nextFireTime;

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Controls.Enable();

        _controls.Controls.Shoot.performed += OnShootPerformed;
        _controls.Controls.Shoot.canceled += OnShootCanceled;
    }

    private void OnDisable()
    {
        _controls.Controls.Shoot.performed -= OnShootPerformed;
        _controls.Controls.Shoot.canceled -= OnShootCanceled;

        _controls.Controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShooting)
        {
            TryFire();                
        }

    }

    private void TryFire()
    {
        // Ainda está no cooldown
        if (Time.time < _nextFireTime)
            return;

        FireBullet();

        _nextFireTime = Time.time + timeBetweenShots;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        _isShooting = true;

        TryFire();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        _isShooting = false;
    }


    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootOffset.position, transform.rotation);

        if (!bullet.TryGetComponent(out Rigidbody2D rb))
        {
            Debug.LogError("O prefab da bala não possui Rigidbody2D.");
            Destroy(bullet);
            return;
        }

        rb.linearVelocity = bulletSpeed * transform.up;

        Debug.Log("Shoot");
    }
}
