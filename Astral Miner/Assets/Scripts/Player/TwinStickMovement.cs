using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class TwinStickMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 5f;

    [Header("Aim")]
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;


    private Vector2 movement;
    private Vector2 aim;
    
    //private Vector3 playerVelocity;

    private CharacterController controller;
    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private Camera mainCamera;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();

        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();        
        HandleRotation();
    }


    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();

    }

    void HandleMovement()
    {
        //GPT - TENTATIVA DE TRANSFORMAÇÃO PARA 2D
        Vector3 move = new Vector3(movement.x, movement.y, 0f);
        if (move.sqrMagnitude > 1f)     move.Normalize(); //evita movimento diagonal mais rápido que verticca/horizontal
        controller.Move(move * Time.deltaTime * playerSpeed);


        /* OLD - VIDEO 3D
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        */
    }

    void HandleRotation()
    {
        if (isGamepad)
        {

            HandleGamepadRotation();

            /* OLD - VIDEO 3D
            //rotate player
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone) //busca valores absolutos de aim para identificar onde está a mira
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.up * aim.y; //identifica a direção

                if(playerDirection.sqrMagnitude > 0.0f) //se a direção mudou
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up); //olha na direção que o stick está movendo
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime); //pan/turn speed
                }
            }
            */

        }
        else
        {
            HandleMouseRotation();

            /* OLD - VIDEO 3D
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if(groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
            */
        }


    }


    void HandleGamepadRotation()
    {
        if (aim.sqrMagnitude < controllerDeadzone * controllerDeadzone)
            return;

        RotateTowards(aim);

    }

    void HandleMouseRotation()
    {
        if (mainCamera == null || Mouse.current == null)
            return;

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint( new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Mathf.Abs(mainCamera.transform.position.z)));


        Vector2 direction = new Vector2(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y);

        if (direction.sqrMagnitude < 0.001f)  return;

        RotateTowards(direction);
    }


    private void RotateTowards(Vector2 direction)
    {
        // Considera que o sprite olha para cima (+Y)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float currentAngle = transform.eulerAngles.z;

        float newAngle = Mathf.MoveTowardsAngle( currentAngle, targetAngle, gamepadRotateSmoothing * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    /*
     private void LookAt(Vector3 lookpoint)
     {
         Vector3 heigtCorrectedPoint = new Vector3(lookpoint.x, transform.position.y, lookpoint.z);
         transform.LookAt(heigtCorrectedPoint);
     }
    */

    public void OnDeviceChange (PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme == "Gamepad";
        //isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }




}
