using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
    public void Back()
    {
        // Simply load the Main Menu when Back is pressed
        SceneManager.LoadScene("StartingScene"); // Change "StartingScene" to your actual Main Menu scene name
    }
}
