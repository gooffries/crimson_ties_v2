using UnityEngine;
using UnityEngine.UI;  // For Button
using UnityEngine.SceneManagement; // For scene management (to load the gameplay scene)

public class IntroPanel : MonoBehaviour
{
    public GameObject introPanel;  // Reference to the "How to Play" intro panel
    public Button playButton;      // Reference to the Play button on the starting menu
    public Button closeButton;     // Reference to the "X" button inside the intro panel

    void Start()
    {
        // Initially hide the intro panel
        introPanel.SetActive(false);

        // Add listener for the Play button to show the intro panel
        playButton.onClick.AddListener(ShowIntroPanel);

        // Add listener for the "X" button to close the intro panel and start the game
        closeButton.onClick.AddListener(CloseIntroPanelAndStartGame);
    }

    // Show the introduction panel when the Play button is clicked
    void ShowIntroPanel()
    {
        introPanel.SetActive(true);  // Show the intro panel
    }

    // Close the introduction panel and start the game when the "X" button is clicked
    void CloseIntroPanelAndStartGame()
    {
        introPanel.SetActive(false);  // Hide the intro panel
        // Optionally, start the game logic or load the gameplay scene
        SceneManager.LoadScene("GameScene"); // Replace with the actual name of your game scene
    }
}
