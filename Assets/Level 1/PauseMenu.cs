using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Ensure menu starts hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("🚀 Resume Button Clicked!");

        // Ensure game unpauses before hiding UI
        Time.timeScale = 1f;
        Debug.Log("✅ Time.timeScale set to: " + Time.timeScale);

        // Hide the pause menu
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Debug.Log("✅ Pause Menu Closed");
        }
        else
        {
            Debug.LogError("❌ Pause Menu UI is NULL!");
        }

        isPaused = false;

        // Reset UI selection to prevent interaction freeze
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }


    public void Pause()
    {
        Debug.Log("Game Paused!"); // Debugging

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GoToSettings()
    {
        Time.timeScale = 1f; // Ensure game is unpaused when switching scenes
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game is unpaused when switching scenes
        SceneManager.LoadScene("StartingScene");
    }
}
