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
        _movingDirection = Move.action.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        if (_sprinting)
        {
            _movingSpeed*=1.5f;
        }
        else {_movingSpeed=player.PlayerGetSpeed();}
            rb.velocity = new Vector3(_movingDirection.x * _movingSpeed, 0, _movingDirection.y * _movingSpeed);
    }
}
