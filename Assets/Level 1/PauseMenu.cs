using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign your Pause Menu Panel here in the Inspector.
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
        Time.timeScale = 1f; // Ensure game is unpaused when switching scenes
        SceneManager.LoadScene("SettingsMenu"); // Replace "SettingsMenu" with your settings scene name
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game is unpaused when switching scenes
        SceneManager.LoadScene("StartingScene"); // Replace "MainMenu" with your main menu scene name
    }
}
