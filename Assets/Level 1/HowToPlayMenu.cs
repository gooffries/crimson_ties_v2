using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    public GameObject howToPlayPanel; // Assign the How To Play panel in the Inspector

    public void ShowHowToPlay()
    {
        // Activate the How To Play panel
        howToPlayPanel.SetActive(true);
        // Optionally, pause the game (if you want to freeze time while the How To Play panel is active)
        Time.timeScale = 0f;
    }

    public void HideHowToPlay()
    {
        // Deactivate the How To Play panel
        howToPlayPanel.SetActive(false);
        // Resume the game
        Time.timeScale = 1f;
    }
}

