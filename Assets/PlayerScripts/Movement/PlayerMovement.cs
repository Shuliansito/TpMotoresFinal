using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
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
    [SerializeField] public GameObject _slider;

    public InputActionReference Move;
    public InputActionReference Run;
    public InputActionReference Interact;
    void Start()
    {
        player = new PlayerClass(5f, 100f);
    }

    private void Update()
    {
        if(_sprinting){
            player.UpdateStamina(25f * Time.deltaTime);
        }

        PlayerStamina=player.GetStamina();

        

        _movingSpeed = player.PlayerGetSpeed();
        _movingDirection = Move.action.ReadValue<Vector2>();
        if (Interact.action.WasPressedThisFrame()) { Debug.Log("Interact"); }

        if (_movingDirection.x != 0 || _movingDirection.y != 0) { _ismoving = true; }
        else { _ismoving = false; }
        if (Run.action.IsPressed() && _ismoving)
        {
            cam.fieldOfView = 63;
            _sprinting=true;
            _movingSpeed = player.PlayerGetSpeed()*1.5f;
        }
        else { cam.fieldOfView = 60; _sprinting = false; }

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
