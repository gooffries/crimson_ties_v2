using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause Menu UI
    public GameObject settingsMenuUI; // Settings Menu UI
    public GameObject helpMenuUI; // Help Menu UI
    private bool isPaused = false;

    void Update()
    {
        // Toggle Pause Menu when pressing "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");

            if (isPaused)
            {
                Debug.Log("Resuming game...");
                Resume();
            }
            else
            {
                Debug.Log("Pausing game...");
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resume button clicked");
        pauseMenuUI.SetActive(false); // Hide Pause Menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;

        // Lock and hide cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        Debug.Log("Pause method called - Showing Pause Menu");
        pauseMenuUI.SetActive(true); // Show Pause Menu
        Time.timeScale = 0f; // Freeze game time
        isPaused = true;

        // Unlock and show cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoToSettings()
    {
        Debug.Log("Settings button clicked - Going to Settings Menu");
        pauseMenuUI.SetActive(false); // Hide Pause Menu
        settingsMenuUI.SetActive(true); // Show Settings Menu
    }

    public void GoToHelp()
    {
        Debug.Log("Help button clicked - Going to Help Menu");
        pauseMenuUI.SetActive(false); // Hide Pause Menu
        helpMenuUI.SetActive(true); // Show Help Menu
    }

    public void GoToMainMenu()
    {
        Debug.Log("Main Menu button clicked - Returning to Main Menu");
        Time.timeScale = 1f; // Unpause game time
        SceneManager.LoadScene("StartingScene"); // Load Main Menu Scene
    }

    public void BackToPauseMenu()
    {
        Debug.Log("Back button clicked - Returning to Pause Menu");
        settingsMenuUI.SetActive(false); // Hide Settings Menu
        helpMenuUI.SetActive(false); // Hide Help Menu
        pauseMenuUI.SetActive(true); // Show Pause Menu
    }
}
