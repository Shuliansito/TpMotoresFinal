using Cryfine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public PlayerClass playerClass = new(5f, 100f);

    [Header("Player Stats")]
    public Rigidbody rb;
    public float PlayerStamina;
    public float PlayerHealth;
    [SerializeField] public bool canRun=false;

    [Header("Movement")]
    [Tooltip("Direction where the player is moving")]
    [SerializeField] private Vector2 _movingDirection;


    [Tooltip("Movement speed")]
    [Range(0f, 10f)]
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _smoothing = 10f;


    [Tooltip("Detects if player is moving")]
    [SerializeField] private float crouchTimer = 0;

    [Tooltip("Detects if player is moving")]
    [SerializeField] private bool _ismoving;

    [Tooltip("Detects if player is crouching")]
    [SerializeField] private bool _iscrouching;


    [Tooltip("Detects if player is sprinting")]
    [SerializeField] private bool _sprinting;


    [Tooltip("Move input action")]
    public InputActionReference Move;

    [Tooltip("Crouch input action")]
    public InputActionReference Crouch;

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
    public InputActionReference Healing;

    #endregion

    public GameObject ManhatanTest;
    public GameObject player;
    private void Start()
    {
        AudioManager.instancia.ReproduceSound("forest_ambience");
        
    }
    void Awake()
    {

        InputSystem.FlushDisconnectedDevices();
        GetComponent<PlayerInput>().defaultControlScheme = null;
        PlayerStamina = playerClass.GetStamina();
        PlayerHealth = playerClass.GetHealth();
    }
    private void OnDrawGizmos()
    {


    }
    private void Update()
    {


        if (Time.timeScale <= 0) { return; }

        playerClass.HealthLimit();


        UpdatePlayerStats();
        MovementLogic();
        CrouchHandle();
        RunningLogic();


        if (Hurt.action.WasPressedThisFrame()) { playerClass.Hurt(10f); }
        if (Healing.action.WasPressedThisFrame()) { playerClass.Healing(10f); }
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

        Vector3 current = rb.velocity;
        
        rb.velocity = new Vector3(
            MathCF.ExpDecay(current.x, direction.x * _movingSpeed, _smoothing, Time.fixedDeltaTime),
            current.y,
            MathCF.ExpDecay(current.z, direction.z * _movingSpeed, _smoothing, Time.fixedDeltaTime)
        );
    }
    private void UpdatePlayerStats()
    {
        PlayerHealth = playerClass.GetHealth();
        PlayerStamina = playerClass.GetStamina();
    }
    private void CrouchHandle()
    {
        if (Crouch.action.WasPressedThisFrame())
        {
            _iscrouching = !_iscrouching;
            if (_iscrouching)
            {
                crouchTimer = 200.0f;
                //Debug.Log(crouchTimer);
            }
        }

        if (_iscrouching)
        {
            crouchTimer = -10.0f * Time.deltaTime;
            crouchTimer = Mathf.Clamp(crouchTimer, 0.0f, 20.0f);
        }

    }
    private void RunningLogic()
    {
        float runBoost = 1.5f;
        float staminaWaste = 20f;

        if (Run.action.IsPressed() && _ismoving && PlayerStamina > 0.5f && canRun)
        {
            cam.fieldOfView = 63;
            _sprinting = true;
            IncreasePlayerMovement(runBoost);
        }
        else
        {
            cam.fieldOfView = 60;
            _sprinting = false;
            ResetPlayerMovement();
        }

        if (_sprinting)
        {
            playerClass.UpdateStamina(-10.0f * Time.deltaTime);
            StaminaSlider.SetActive(true);
        }
        if(!_sprinting&&_ismoving)
            playerClass.UpdateStamina(staminaWaste/2 * Time.deltaTime);
        else if (!_sprinting)
            playerClass.UpdateStamina(staminaWaste * Time.deltaTime);
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
    public void IncreasePlayerMovement(float amount)
    {
        _movingSpeed = playerClass.GetSpeed() * amount;
    }

    public void DecreasePlayerMovement(float amount)
    {
        _movingSpeed = playerClass.GetSpeed() / amount;
    }
    public void ResetPlayerMovement()
    {
        _movingSpeed = playerClass.GetSpeed();
    }
}
