using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause Menu UI
    public GameObject settingsMenuUI; // Settings Menu UI (in the same canvas as the Pause Menu)
    private bool isPaused = false;

    void Update()
    {
        // Toggle Pause Menu when pressing "Escape"
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
        pauseMenuUI.SetActive(false); // Hide Pause Menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show Pause Menu
        Time.timeScale = 0f; // Freeze game time
        isPaused = true;
    }

    public void GoToSettings()
    {
        pauseMenuUI.SetActive(false); // Hide Pause Menu
        settingsMenuUI.SetActive(true); // Show Settings Menu (on the same canvas)
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Unpause game time
        SceneManager.LoadScene("StartingScene"); // Load Main Menu Scene
    }

    public void BackToPauseMenu()
    {
        settingsMenuUI.SetActive(false); // Hide Settings Menu
        pauseMenuUI.SetActive(true); // Show Pause Menu
    }
}
