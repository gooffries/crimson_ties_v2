using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGamePaused = false; // Track the game's paused state

    [Header("Player Stats")]
    public float playerHealth = 100f; // ✅ Player health persists
    public float swordStrength = 10f; // ✅ Sword strength persists
    public int gemsCollected = 0;     // ✅ Gems persist across levels

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ✅ Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // ✅ Destroy duplicate instances
        }
    }

    /// <summary>
    /// Loads the next level while keeping player stats.
    /// </summary>
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // ✅ Loads next level
        }
        else
        {
            Debug.LogWarning("No more levels!"); // ✅ Prevents out-of-range error
        }
    }

    /// <summary>
    /// Updates sword strength when leveling up.
    /// </summary>
    public void UpdateSwordStrength(float newStrength)
    {
        swordStrength = newStrength;
    }

    /// <summary>
    /// Adds collected gems and persists the total.
    /// </summary>
    public void AddGems(int gemValue)
    {
        gemsCollected += gemValue;
    }

    // You can toggle the paused state and call this method from your pause menu logic
    public void TogglePauseMenu()
    {
        IsGamePaused = !IsGamePaused;

        // Optionally, activate/deactivate the pause menu here
    }
}
