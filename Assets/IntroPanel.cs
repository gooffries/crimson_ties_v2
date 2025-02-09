using UnityEngine;
using UnityEngine.UI;

public class IntroPanel : MonoBehaviour
{
    public GameObject introPanel;  // Reference to the Intro Panel UI
    public Button closeButton;     // Reference to the "X" button

    void Start()
    {
        // Show the intro panel and unlock the cursor when the game scene loads
        introPanel.SetActive(true);
        Cursor.visible = true; // Make the cursor visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor

        // Add listener for the "X" button to close the intro panel
        closeButton.onClick.AddListener(CloseIntroPanel);
    }

    // Close the Introduction Panel and start the game
    void CloseIntroPanel()
    {
        introPanel.SetActive(false);  // Hide the intro panel

        // Lock and hide the cursor for gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
