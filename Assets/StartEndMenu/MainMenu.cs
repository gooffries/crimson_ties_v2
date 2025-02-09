using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function to start the game
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1 copy"); // Replace with your actual game scene name
    }

    // Function to exit the game
    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // Log for testing in Unity Editor
        Application.Quit(); // Quits the application (works in a built game)
    }
}
