using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilPrefab;

    [SerializeField]
    private InputActionReference Shoot;

    [SerializeField]
    private int shootForce=15;
    public Camera cam;

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        DetectarErrores();
        //Debug.Log("Detecting Shooting");
        if (Shoot.action.WasPressedThisFrame())
        {
            if (Gamepad.current == null) { Debug.LogWarning("[Input system] Gamepad not detected"); }
            //Debug.Log("Start shooting");
            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            GameObject pro;
            pro = Instantiate(projectilPrefab, ray.origin, transform.rotation);
            //Debug.Log("Projectil instantiated");
            Rigidbody rb = pro.GetComponent<Rigidbody>();
            //Debug.Log("Rigidbogy created");
            rb.AddForce(cam.transform.forward * shootForce, ForceMode.Impulse);




            Destroy(pro, 2);
        }
    }

    private void DetectarErrores()
    {
        if (Shoot == null) { Debug.LogError("'Shoot' Input reference not found"); }
        if (cam == null) { Debug.LogError("'cam' Camera not found"); }
        if (projectilPrefab == null) { Debug.LogError("'projectilPrefabl' Prefab not found"); }
    }
}
