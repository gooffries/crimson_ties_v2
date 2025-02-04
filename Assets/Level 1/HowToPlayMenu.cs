using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    public GameObject howToPlayPanel; // Reference to the Help Panel

    public void ShowHowToPlay()
    {
        Debug.Log("Help button clicked - Showing How To Play Panel");

        // Hide Pause Menu and show How To Play Panel
        pauseMenuUI.SetActive(false);
        howToPlayPanel.SetActive(true);

        // Pause game time
        Time.timeScale = 0f;

        // Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideHowToPlay()
    {
        Debug.Log("Back button clicked - Returning to Pause Menu");

        // Hide How To Play Panel and show Pause Menu
        howToPlayPanel.SetActive(false);
        pauseMenuUI.SetActive(true);

        // Game is still paused, so don't resume time here
    }
}
