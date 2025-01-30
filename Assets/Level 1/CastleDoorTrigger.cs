using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [Header("Set the scene to load in the Inspector")]
    public string nextSceneName; // ✅ Scene name is now set from the Inspector
    void Start()
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("❌ DoorTrigger is DISABLED in the hierarchy!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🚪 Something entered the trigger: {other.name}"); // ✅ Check if anything enters

        if (other.CompareTag("Player")) // ✅ Ensure the player tag matches
        {
            Debug.Log($"✅ Player entered the door! Loading scene: {nextSceneName}");
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("❌ Scene name is missing in the Inspector!");
            }
        }
    }

}
