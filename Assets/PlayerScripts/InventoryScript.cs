using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private bool isOnInventory;
    [SerializeField] private GameObject _inventoryMenu;

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        bool pressed = (keyboard != null && keyboard.tabKey.wasPressedThisFrame)
                    || (gamepad != null && gamepad.dpad.left.wasPressedThisFrame);

        bool released = (keyboard != null && keyboard.tabKey.wasReleasedThisFrame)
                     || (gamepad != null && gamepad.dpad.left.wasReleasedThisFrame);

        if (pressed && !UIManager.instance._isPaused) { OpenInventory(); }
        if (released && isOnInventory) { CloseInventory(); }
    }

    private void OpenInventory()
    {
        isOnInventory = true;
        _inventoryMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    private void CloseInventory()
    {
        isOnInventory = false;
        _inventoryMenu.SetActive(false);

        if (UIManager.instance._isPaused) { return; }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}