using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderWater : MonoBehaviour
{
    [SerializeField] private GameObject _underWaterImage;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private bool _isUnderWater=false;
    [SerializeField] private float _speedDebufUnderWater=2f;
    [SerializeField] private PlayerMovement _playerMovement;
    public float _waterLimitPosition;


    private void Start()
    {
        _playerMovement=GetComponent<PlayerMovement>();
    }
    void Update()
    { 
        _underWaterImage.SetActive(_isUnderWater);
        bool isUnderWater = transform.position.y <= _waterLimitPosition;

        if (_isUnderWater != isUnderWater)
        {
            _isUnderWater = isUnderWater;
            UpdateUnderWaterState();
        }
        
    }
    private void UpdateUnderWaterState()
    {
        _playerMovement.canRun = !_isUnderWater;

        _playerCamera.fieldOfView = _isUnderWater ? 60f : 55f;

        if (_isUnderWater)
            _playerMovement.DecreasePlayerMovement(_speedDebufUnderWater);
        else
            _playerMovement.ResetPlayerMovement();
    }
}


