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
    public PlayerClass player = new(5f, 100f);

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
        PlayerStamina = player.GetStamina();
        PlayerHealth = player.PlayerGetHealth();

    }

    private void Update()
    {
        player.HealthLimit();

        UpdatePlayerStats();
        MovementLogic();
        RunningLogic();

        
        if (Hurt.action.WasPressedThisFrame()) { player.Hurt(10f); }

        player.DetectDeath();
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
        PlayerHealth = player.PlayerGetHealth();
        PlayerStamina = player.GetStamina();
    }

    private void RunningLogic()
    {
        

        if (Run.action.IsPressed() && _ismoving && PlayerStamina > 0.5)
        {
            cam.fieldOfView = 63;
            _sprinting = true;
            _movingSpeed = player.PlayerGetSpeed() * 1.5f;
        }
        else
        {
            cam.fieldOfView = 60;
            _sprinting = false;
            _movingSpeed = player.PlayerGetSpeed();
        }

        if (_sprinting)
        {
            player.UpdateStamina(-10.0f * Time.deltaTime);
            StaminaSlider.SetActive(true);
        }

        if (!_sprinting && !_ismoving)
        {
            player.UpdateStamina(20.0f * Time.deltaTime);
        }
    }

    private void MovementLogic()
    {
        _movingDirection = Move.action.ReadValue<Vector2>();

        if (_movingDirection.x != 0 || _movingDirection.y != 0)
        {
            _ismoving = true;
        }
        else
        {
            _ismoving = false;
        }
    }


    
}
