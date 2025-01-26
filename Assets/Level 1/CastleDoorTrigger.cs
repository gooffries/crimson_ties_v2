using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    // The name of the scene to load when the player enters the door.
    public string nextSceneName = "level2"; // Change this to the name of your King's Room scene

    // When the player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player object is tagged as "Player"
        {
            // Load the next scene
            SceneManager.LoadScene("level2");
        }
    }
}
