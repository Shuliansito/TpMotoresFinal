using Cryfine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference Shoot;
    [SerializeField] private float _lightDuration = 0.2f;
    [SerializeField] private GameObject _shootingLight;
    [SerializeField] private float _damage = 25f;
    [SerializeField] private float _range = 50f;
    public Camera cam;

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        DetectarErrores();

        if (Shoot.action.WasPressedThisFrame())
        {
            ShootRay();
            ShootingEffects();
        }
    }

    private void ShootRay()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, _range))
        {
            DebugCF.DrawDistance(ray.origin, hit.point, Color.red, 1f);

            ZombieHealth zombie = hit.collider.GetComponentInParent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(_damage);
            }
        }
        else
        {
            DebugCF.DrawDistance(ray.origin, ray.origin + ray.direction * _range, Color.yellow, 1f);
        }
    }


    private void ShootingEffects()
    {
        AudioManager.instancia.ReproduceSound("shoot_fired");
        _shootingLight.SetActive(true);
        Invoke(nameof(HideLight), 0.1f);

        
    }

    private void HideLight()
    {
        _shootingLight.SetActive(false);
    }

    private void DetectarErrores()
    {
        if (Shoot == null) { Debug.LogError("'Shoot' Input reference not found"); }
        if (cam == null) { Debug.LogError("'cam' Camera not found"); }
    }
}