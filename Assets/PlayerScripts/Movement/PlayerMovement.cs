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


    public Rigidbody rb;
    public Camera cam;
    [SerializeField]private Vector2 _movingDirection;
    [SerializeField]private bool _sprinting;
    [SerializeField]private bool _ismoving;
    [SerializeField]private bool _interacting;
    [SerializeField]private float _movingSpeed;
    public float PlayerStamina;
    public GameObject StaminaSlider;
    public float PlayerHealth;
    public GameObject HealthSlider;
    public PlayerClass player = new(5f, 100f);
    public GameObject _dotCrosshair;
    public GameObject _interactCrosshair;

    public InputActionReference Move;
    public InputActionReference Run;
    public InputActionReference Hurt;


    void Awake()
    {
        PlayerStamina = player.GetStamina();
        PlayerHealth = player.PlayerGetHealth();

    }

    private void Update()
    {
        player.HealthLimit();
        
        #region Movement
        _movingDirection = Move.action.ReadValue<Vector2>();

        if (_movingDirection.x != 0 || _movingDirection.y != 0)
        {
            _ismoving = true;
        }
        else
        {
            _ismoving = false;
        }


            #endregion

            #region Sprinting & Stamina

            PlayerStamina = player.GetStamina();

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

        if (!_sprinting&&!_ismoving)
        {
            player.UpdateStamina(20.0f * Time.deltaTime);
        }


        #endregion

        #region Health

        PlayerHealth =player.PlayerGetHealth();

        #endregion

        if (Hurt.action.WasPressedThisFrame()) { player.Hurt(10f); }
        if (PlayerHealth <= 0) { SceneManager.LoadScene(sceneName: "Prototype Map"); }
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
