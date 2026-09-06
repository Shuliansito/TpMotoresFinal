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
    private Vector2 _movingDirection;
    private bool _sprinting;
    private float _movingSpeed;

    public InputActionReference Move;
    public InputActionReference Run;
    void Start()
    {
        player = new PlayerClass(5f, 100f);
    }

    private void Update()
    {
        

        _sprinting = Run.action.IsPressed();
        _movingSpeed = player.PlayerGetSpeed();
        _movingDirection = Move.action.ReadValue<Vector2>();
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
