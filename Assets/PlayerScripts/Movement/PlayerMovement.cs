using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerClass player;

    public Rigidbody rb;
    public Camera cam;
    [SerializeField]private Vector2 _movingDirection;
    [SerializeField]private bool _sprinting;
    [SerializeField]private bool _ismoving;
    [SerializeField]private bool _interacting;
    [SerializeField]private float _movingSpeed;
    [SerializeField] public float PlayerStamina;
    [SerializeField] public GameObject StaminaSlider;

    public InputActionReference Move;
    public InputActionReference Run;
    public InputActionReference Interact;
    void Start()
    {
        player = new PlayerClass(5f, 100f);
    }

    private void Update()
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

        #region Sprinting & Stamina

        PlayerStamina = player.GetStamina();

        if (Run.action.IsPressed() && _ismoving && PlayerStamina > 0)
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
            player.UpdateStamina(-25.0f * Time.deltaTime);
            StaminaSlider.SetActive(true);
        }

        if (!_sprinting && !_ismoving)
        {
            player.UpdateStamina(10.0f * Time.deltaTime);
        }

        PlayerStamina = player.GetStamina();

        #endregion

        if (Interact.action.WasPressedThisFrame())
        {
            Debug.Log("Interact");
        }
    }
    void FixedUpdate()
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
    
}
