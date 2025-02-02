using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
    }

    public void GoToSettingsScene()
    {
        MenuManager.previousMenu = "MainMenu"; // Store previous menu before switching scenes
        SceneManager.LoadScene("SettingsMenu");
    }

    public void QuitScene()
    {
        Application.Quit();
    }
}
