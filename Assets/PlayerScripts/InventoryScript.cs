using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class InventoryItem
{
    public string name;
    public bool isGun;
    public GameObject item;
    public GameObject button;
    public bool unlocked;
}

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private bool isOnInventory;
    [SerializeField] private GameObject _inventoryMenu;
    [SerializeField] private InventoryItem[] _items;

    private void Start()
    {
        EquipItem(-1);
    }

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

    public void SelectItem(int index)
    {
        if (index < 0 || index >= _items.Length) { return; }
        if (!_items[index].unlocked) { return; }

        EquipItem(index);
        CloseInventory();
    }

    public void UnlockItem(int index)
    {
        _items[index].unlocked = true;
    }

    private void EquipItem(int index)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].item.SetActive(i == index);
        }

        if (UIManager.instance != null)
        {
            UIManager.instance._isUsingAGun = index >= 0 && _items[index].isGun;
        }
    }

    private void RefreshButtons()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].button.SetActive(_items[i].unlocked);
        }
    }

    private void OpenInventory()
    {
        RefreshButtons();

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