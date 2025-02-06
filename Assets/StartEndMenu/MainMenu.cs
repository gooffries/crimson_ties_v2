using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1 copy"); // Load next scene
    }

    public void QuitScene()
    {
        Application.Quit();
    }
}
