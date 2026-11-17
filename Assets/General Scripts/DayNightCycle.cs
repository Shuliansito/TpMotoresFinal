using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public float _rotationScale = 2f;

    [SerializeField] private GameObject _spawners;  
    [SerializeField] private TextMeshProUGUI _dayCounter;  
    [SerializeField] private bool isNight;   
    [SerializeField] private float day;
    [SerializeField] private float _daysPassed;
    [SerializeField] public int _currentDay = 1;
    public float _totalRotation;


    [Header("Sun")]
    [SerializeField] public float _dayIntensity = 1f;
    [SerializeField] public float _nightIntensity = 0f;

    [Header("Ambient")]
    [SerializeField] public Color _dayAmbient = new Color(0.6f, 0.6f, 0.6f);
    [SerializeField] public Color _nightAmbient = new Color(0.08f, 0.1f, 0.2f);

    [Header("Fog")]
    [SerializeField] public Color _dayFogColor = new Color(0.7f, 0.8f, 0.9f);
    [SerializeField] public Color _nightFogColor = new Color(0.05f, 0.07f, 0.12f);
    [SerializeField] public float _dayFogDensity = 0.005f;
    [SerializeField] public float _nightFogDensity = 0.03f;

    private Light _sun;

    void Awake()
    {
        _sun = GetComponent<Light>();
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.ambientMode = AmbientMode.Flat;
    }

    void Update()
    {

        float rotation = _rotationScale * Time.deltaTime;
        transform.Rotate(rotation, 0, 0);

        _totalRotation += rotation;
        _daysPassed = _totalRotation / 360f;
        _currentDay = Mathf.FloorToInt(_daysPassed) + 1;

        _dayCounter.text = "- DÍA "+_currentDay.ToString()+" -";

        day = Mathf.InverseLerp(-0.2f, 0.2f, -transform.forward.y);

        isNight = day < 0.5f;

        ZombieSpawner.IsNight = isNight;
        _sun.intensity = Mathf.Lerp(_nightIntensity, _dayIntensity, day);
        RenderSettings.ambientLight = Color.Lerp(_nightAmbient, _dayAmbient, day);
        RenderSettings.fogColor = Color.Lerp(_nightFogColor, _dayFogColor, day);
        RenderSettings.fogDensity = Mathf.Lerp(_nightFogDensity, _dayFogDensity, day);

        _spawners.SetActive(isNight);

    }
}