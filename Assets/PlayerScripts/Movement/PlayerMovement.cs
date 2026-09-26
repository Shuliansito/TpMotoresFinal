using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public PlayerClass playerClass = new(5f, 100f);

    [Header("Player Stats")]
    public Rigidbody rb;
    public float PlayerStamina;
    public float PlayerHealth;
    
    

    [Header ("Movement")]
    [Tooltip("Direction where the player is moving")]
    [SerializeField] private Vector2 _movingDirection;


    [Tooltip("Movement speed")]
    [Range(0f, 10f)]
    [SerializeField] private float _movingSpeed;


    [Tooltip("Detects if player is moving")]
    [SerializeField] private bool _ismoving;


    [Tooltip("Detects if player is sprinting")]
    [SerializeField] private bool _sprinting;


    [Tooltip("Move input action")]
    public InputActionReference Move;


    [Tooltip("Run input action")]
    public InputActionReference Run;



    

    [Header("UI References")]

    public GameObject StaminaSlider;
    public GameObject HealthSlider;
    public Camera cam;
    public GameObject _dotCrosshair;
    public GameObject _interactCrosshair;

    [Header("Place Holders")]
    [SerializeField] private bool _interacting;
    public InputActionReference Hurt;


    void Awake()
    {
        PlayerStamina = playerClass.GetStamina();
        PlayerHealth = playerClass.PlayerGetHealth();

    }

    private void Update()
    {
        playerClass.HealthLimit();

        UpdatePlayerStats();
        MovementLogic();
        RunningLogic();

        
        if (Hurt.action.WasPressedThisFrame()) { playerClass.Hurt(10f); }


    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 direction =
            transform.right * _movingDirection.x +
            transform.forward * _movingDirection.y;

        rb.velocity = new Vector3(
            direction.x * _movingSpeed,
            rb.velocity.y,
            direction.z * _movingSpeed
        );
    }

    private void UpdatePlayerStats()
    {
        PlayerHealth = playerClass.PlayerGetHealth();
        PlayerStamina = playerClass.GetStamina();
    }

    private void RunningLogic()
    {
        

        if (Run.action.IsPressed() && _ismoving && PlayerStamina > 0.5)
        {
            cam.fieldOfView = 63;
            _sprinting = true;
            _movingSpeed = playerClass.PlayerGetSpeed() * 1.5f;
        }
        else
        {
            cam.fieldOfView = 60;
            _sprinting = false;
            _movingSpeed = playerClass.PlayerGetSpeed();
        }

        if (_sprinting)
        {
            playerClass.UpdateStamina(-10.0f * Time.deltaTime);
            StaminaSlider.SetActive(true);
        }

        if (!_sprinting && !_ismoving)
        {
            playerClass.UpdateStamina(20.0f * Time.deltaTime);
        }
    }

    private void MovementLogic()
    {
        Vector2 keyboardInput = Move.action.ReadValue<Vector2>();

        Vector2 stickInput = Vector2.zero;
        if (Gamepad.current != null)
            stickInput = Gamepad.current.leftStick.ReadValue();
        
        _movingDirection = keyboardInput + stickInput;
        _movingDirection = Vector2.ClampMagnitude(_movingDirection, 1f);

        _ismoving = _movingDirection.x != 0 || _movingDirection.y != 0;
    }



}
