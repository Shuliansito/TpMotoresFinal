using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpLogic : MonoBehaviour
{
    [SerializeField] private float _energyWaste;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private InputActionReference jumpInputAction;
    private PlayerMovement _playerMovement;
    private Rigidbody rb;


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (jumpInputAction.action.WasPressedThisFrame() && _playerMovement.IsGrounded()&&_playerMovement.PlayerStamina>=25)
        {
            float jumpSpeed = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            _playerMovement.playerClass.UpdateStamina(-_energyWaste);
        }
    }

}
