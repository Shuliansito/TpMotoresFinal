using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public InputActionReference _pauseMenu;
    private bool _isPaused;
    public GameObject pauseUI;

    private void Update()
    {
        if (_pauseMenu.action.WasPressedThisFrame())
        {
            UpdateCursorState();
            pauseUI.SetActive(_isPaused);
        }
    }

    private void UpdateCursorState()
    {
        _isPaused = !_isPaused;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isPaused;
        Time.timeScale = _isPaused ? 0.0f : 1.0f;
    }

    public void OnResume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        UpdateCursorState();
    }
    public void OnExit()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
}
