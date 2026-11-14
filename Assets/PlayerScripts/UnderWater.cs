
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cryfine;
using UnityEngine.UI;

public class UnderWater : MonoBehaviour
{
    public float _playerOxigen = 100f;

    [SerializeField] private GameObject _underWaterImage;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private bool _isUnderWater = false;
    [SerializeField] private float _speedDebufUnderWater = 2f;
    [SerializeField] private float _healDebufUnderWater = 15f;
    [SerializeField] private PlayerMovement _playerMovement;
    public float _waterLimitPosition;

    public bool IsUnderWater => _isUnderWater;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        bool isUnderWater = transform.position.y <= _waterLimitPosition;

        if (_isUnderWater != isUnderWater)
        {
            _isUnderWater = isUnderWater;
            UpdateUnderWaterState();
        }

        if (_isUnderWater)
        {
            _playerOxigen -= _healDebufUnderWater * Time.deltaTime;
            _playerCamera.fieldOfView = 55f;
        }
        else
        {
            _playerOxigen += _healDebufUnderWater / 2 * Time.deltaTime;
            _playerCamera.fieldOfView = 60f;
        }

        _playerOxigen = MathCF.Clamp(_playerOxigen, 0f, 100f);

        UIManager.instance._oxigenSlider.value = _playerOxigen;
        _underWaterImage.SetActive(_isUnderWater);
    }

    private void UpdateUnderWaterState()
    {
        if (_isUnderWater)
        {
            AudioManager.instancia.ReproduceSound("under_water");
            
        }
        else
        {
            AudioManager.instancia.PauseSound("under_water");
            
        }

        _playerMovement.canRun = !_isUnderWater;
    }
}
