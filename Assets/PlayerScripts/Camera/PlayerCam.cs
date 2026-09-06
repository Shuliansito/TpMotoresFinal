using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 0.1f;

    private float xRotation = 0f;
    private Transform playerBody;

    private void Awake()
    {
        playerBody = transform.root;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mouseLook = Mouse.current.delta.ReadValue();

        float mouseX = mouseLook.x * mouseSensitivity;
        float mouseY = mouseLook.y * mouseSensitivity;

        // Rotación vertical de la cámara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal del jugador
        playerBody.Rotate(Vector3.up * mouseX);
    }
}