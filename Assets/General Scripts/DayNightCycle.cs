using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public float _rotationScale = 2f;

    [Header("Sun")]
    [SerializeField] private float _dayIntensity = 1f;
    [SerializeField] private float _nightIntensity = 0f;

    [Header("Ambient")]
    [SerializeField] private Color _dayAmbient = new Color(0.6f, 0.6f, 0.6f);
    [SerializeField] private Color _nightAmbient = new Color(0.08f, 0.1f, 0.2f);

    [Header("Fog")]
    [SerializeField] private Color _dayFogColor = new Color(0.7f, 0.8f, 0.9f);
    [SerializeField] private Color _nightFogColor = new Color(0.05f, 0.07f, 0.12f);
    [SerializeField] private float _dayFogDensity = 0.005f;
    [SerializeField] private float _nightFogDensity = 0.03f;

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
        transform.Rotate(_rotationScale * Time.deltaTime, 0, 0);

        float day = Mathf.InverseLerp(-0.2f, 0.2f, -transform.forward.y);

        ZombieSpawner.IsNight = day < 0.5f;
        _sun.intensity = Mathf.Lerp(_nightIntensity, _dayIntensity, day);
        RenderSettings.ambientLight = Color.Lerp(_nightAmbient, _dayAmbient, day);
        RenderSettings.fogColor = Color.Lerp(_nightFogColor, _dayFogColor, day);
        RenderSettings.fogDensity = Mathf.Lerp(_nightFogDensity, _dayFogDensity, day);
    }
}