using Cryfine;
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
    [SerializeField] public bool canRun = false;

    [Header("Movement")]
    [Tooltip("Direction where the player is moving")]
    [SerializeField] private Vector2 _movingDirection;

    [Tooltip("Current speed, starts at 0 and rises toward the max speed")]
    [Range(0f, 10f)]
    [SerializeField] private float _movingSpeed = 0f;

    [Tooltip("Speed limit right now: 0 idle, walk speed or run speed")]
    [SerializeField] private float _maxActualSpeed = 0f;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 7.5f;

    [Tooltip("How fast the speed reaches the max (higher = faster)")]
    [SerializeField] private float _acceleration = 6f;

    [Tooltip("How smooth the velocity follows the input (higher = snappier)")]
    [SerializeField] private float _smoothing = 10f;

    [Tooltip("Crouch timer")]
    [SerializeField] private float crouchTimer = 0;

    [Tooltip("Detects if player is moving")]
    [SerializeField] private bool _ismoving;

    [Tooltip("Detects if player is crouching")]
    [SerializeField] private bool _iscrouching;

    [Tooltip("Detects if player is sprinting")]
    [SerializeField] private bool _sprinting;
    [SerializeField] private float _staminaDrain = 10f;
    [SerializeField] private float _staminaRecoverThreshold = 20f;
    private bool _exhausted;

    [Header("Camera FOV")]
    [SerializeField] private float _walkFov = 60f;
    [SerializeField] private float _runFov = 63f;
    [SerializeField] private float _fovSmoothing = 8f;

    [Header("Footsteps")]
    [Tooltip("Metros entre paso y paso caminando")]
    [SerializeField] private float _walkStride = 2.5f;
    [Tooltip("Metros entre paso y paso corriendo")]
    [SerializeField] private float _runStride = 3f;
    [Tooltip("Por debajo de esta velocidad no suenan pasos")]
    [SerializeField] private float _minStepSpeed = 0.5f;
    [SerializeField] private float _groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask _groundMask = ~0;
    private float _stepDistance;
    private bool _leftFoot;

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

    void Awake()
    {
        InputSystem.FlushDisconnectedDevices();
        GetComponent<PlayerInput>().defaultControlScheme = null;
        PlayerStamina = playerClass.GetStamina();
        PlayerHealth = playerClass.GetHealth();
    }

    private void Update()
    {
        if (Time.timeScale <= 0) { return; }

        playerClass.HealthLimit();

        UpdatePlayerStats();
        MovementLogic();
        CrouchHandle();
        RunningLogic();
        SpeedLogic();
        FootstepsSoundLogic();
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
    public void AddSpeed(float amount)
    {
        _walkSpeed += amount;
        _runSpeed += amount;
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
            }
        }

        if (_iscrouching)
        {
            crouchTimer -= 10.0f * Time.deltaTime;
            crouchTimer = Mathf.Clamp(crouchTimer, 0.0f, 20.0f);
        }
    }

    private void MovementLogic()
    {
        Vector2 keyboardInput = Move.action.ReadValue<Vector2>();

        Vector2 stickInput = Vector2.zero;
        if (Gamepad.current != null)
            stickInput = Gamepad.current.leftStick.ReadValue();

        _movingDirection = Vector2.ClampMagnitude(keyboardInput + stickInput, 1f);
        _ismoving = _movingDirection != Vector2.zero;
    }

    private void RunningLogic()
    {
        float staminaDrain = 10f;
        float staminaRecover = 20f;

        if (PlayerStamina <= 0.5f) { _exhausted = true; }
        if (PlayerStamina >= _staminaRecoverThreshold) { _exhausted = false; }

        _sprinting = Run.action.IsPressed() && _ismoving && canRun && !_exhausted;

        float targetFov = _sprinting ? _runFov : _walkFov;
        cam.fieldOfView = MathCF.ExpDecay(cam.fieldOfView, targetFov, _fovSmoothing, Time.deltaTime);

        if (_sprinting)
        {
            playerClass.UpdateStamina(-_staminaDrain * Time.deltaTime);
            StaminaSlider.SetActive(true);
        }
        else if (_ismoving)
            playerClass.UpdateStamina(staminaRecover / 2 * Time.deltaTime);
        else
            playerClass.UpdateStamina(staminaRecover * Time.deltaTime);
    }
    public void ReduceStaminaDrain(float percent)
    {
        _staminaDrain *= 1f - percent;
    }
    private void SpeedLogic()
    {
        if (!_ismoving)
        {
            _maxActualSpeed = 0f;
            _movingSpeed = 0f;
            return;
        }

        _maxActualSpeed = _sprinting ? _runSpeed : _walkSpeed;
        _movingSpeed = MathCF.ExpDecay(_movingSpeed, _maxActualSpeed, _acceleration, Time.deltaTime);
    }

    public bool IsGrounded()
    {
       return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance,
                        _groundMask, QueryTriggerInteraction.Ignore);
    }
        

    private void FootstepsSoundLogic()
    {
        Vector3 vel = rb.velocity;
        float horizontalSpeed = new Vector2(vel.x, vel.z).magnitude;
        float stride = _sprinting ? _runStride : _walkStride;

        if (horizontalSpeed < _minStepSpeed || !IsGrounded())
        {
            _stepDistance = stride * 0.5f;
            return;
        }

        _stepDistance += horizontalSpeed * Time.deltaTime;

        if (_stepDistance >= stride)
        {
            _stepDistance -= stride;
            AudioManager.instancia.ReproduceSound(_leftFoot ? "footsteps_1" : "footsteps_2");
            _leftFoot = !_leftFoot;
        }
    }
}