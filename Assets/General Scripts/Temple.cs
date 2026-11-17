using UnityEngine;
using UnityEngine.SceneManagement;

public class Temple : MonoBehaviour
{
    public static bool GameWon;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _missingPanel;

    private void Start()
    {
        ArtifactPickup.Collected = 0;
        GameWon = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>() == null) { return; }

        if (ArtifactPickup.Collected >= ArtifactPickup.Total)
        {
            Win();
        }
        else
        {
            _missingPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>() == null) { return; }

        _missingPanel.SetActive(false);
    }

    private void Win()
    {
        GameWon = true;
        _missingPanel.SetActive(false);
        _winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}