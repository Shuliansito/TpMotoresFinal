
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ColorUtility = UnityEngine.ColorUtility;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject _playerGO;
    public InputActionReference _pauseMenu;
    private bool _isPaused;
    public GameObject pauseUI;
    public GameObject HealthSliderGO;
    public GameObject StaminaSliderGO;
    public GameObject OxigenSliderGO;
    public PlayerMovement _player;
    [SerializeField] private Image _staminaFill;
    public Slider _staminaSlider;
    public Slider _healthSlider;
    public Slider _oxigenSlider;

    [SerializeField] private Image _underWaterImage;

    [SerializeField] private float StaminaTimer = -1;
    [SerializeField] private float _lastStamina;
    [SerializeField] private float HealthTimer = -1;
    [SerializeField] private float _lastHealth;
    [SerializeField] private float OxigenTimer = -1;
    [SerializeField] private float _lastOxigen;

    [SerializeField] private InputActionAsset _inputActions;

    private UnderWater _underWater;

    private void OnEnable()
    {
        _inputActions.FindActionMap("UI").Enable();
        _inputActions.FindActionMap("Pause Menu").Enable();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _lastOxigen = _oxigenSlider.value;
        _lastHealth = _player.playerClass.GetHealth();
        _lastStamina = _player.playerClass.GetStamina();
        _underWater = _player.GetComponent<UnderWater>();
        
    }

    private void Update()
    {
        if (_pauseMenu.action.WasPressedThisFrame())
        {
            UnityEngine.Debug.Log("Escape/Home apretado");
            UpdateCursorState();
            pauseUI.SetActive(_isPaused);
        }

        _staminaSlider.value = _player.PlayerStamina;
        _healthSlider.value = _player.PlayerHealth;

        #region Stamina

        if (_player.PlayerStamina != _lastStamina)
        {
            StaminaTimer = 2.0f;
            StaminaSliderGO.SetActive(true);
        }

        if (StaminaTimer > 0)
        {
            StaminaTimer -= Time.deltaTime;
        }
        else
        {
            StaminaSliderGO.SetActive(false);
        }

        _lastStamina = _player.PlayerStamina;

        #endregion

        #region Health

        if (_player.PlayerHealth != _lastHealth)
        {
            HealthTimer = 5.0f;
            HealthSliderGO.SetActive(true);
        }

        if (HealthTimer > 0)
        {
            HealthTimer -= Time.deltaTime;
        }
        else
        {
            HealthSliderGO.SetActive(false);
        }

        _lastHealth = _player.PlayerHealth;

        #endregion

        #region Oxigen

        if (_oxigenSlider.value != _lastOxigen)
        {
            OxigenTimer = 5.0f;
            OxigenSliderGO.SetActive(true);
        }

        if (_underWater.IsUnderWater)
        {
            OxigenSliderGO.SetActive(true);
        }
        else if (OxigenTimer > 0)
        {
            OxigenTimer -= Time.deltaTime;
        }
        else
        {
            OxigenSliderGO.SetActive(false);
        }

        _lastOxigen = _oxigenSlider.value;

        #endregion

        RectTransform staminaRect = StaminaSliderGO.GetComponent<RectTransform>();

        if (HealthSliderGO.activeSelf)
        {
            staminaRect.anchoredPosition = new Vector2(
                staminaRect.anchoredPosition.x,
                -60
            );
        }
        else
        {
            staminaRect.anchoredPosition = new Vector2(
                staminaRect.anchoredPosition.x,
                0
            );
        }
    }

    private void UpdateCursorState()
    {
        _isPaused = !_isPaused;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isPaused;
        Time.timeScale = _isPaused ? 0.0f : 1.0f;

        if (_isPaused)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void OnResume()
    {
        _isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnExit()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
}
