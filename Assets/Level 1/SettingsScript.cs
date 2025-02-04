using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // UI Elements
    public GameObject settingsPanel;  // Settings Panel
    public GameObject pausePanel;     // Pause Panel

    public Slider backgroundSoundSlider;
    public Slider soundEffectsSlider;

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    void Start()
    {
        // Initialize settings (load from PlayerPrefs if available)
        backgroundSoundSlider.value = PlayerPrefs.GetFloat("BackgroundSound", 1f);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffects", 1f);
        AudioListener.volume = backgroundSoundSlider.value;

        // Button click listeners for difficulty level (functionality is disabled for now)
        easyButton.onClick.AddListener(() => SetDifficulty(0));
        normalButton.onClick.AddListener(() => SetDifficulty(1));
        hardButton.onClick.AddListener(() => SetDifficulty(2));
    }

    // Handle difficulty button clicks (for show only)
    void SetDifficulty(int difficulty)
    {
        // No functionality for difficulty right now, just a placeholder.
        // You can later implement this when you're ready.
    }

    // Apply settings (background sound and sound effects only)
    public void ApplySettings()
    {
        // Save background sound and sound effects
        PlayerPrefs.SetFloat("BackgroundSound", backgroundSoundSlider.value);
        AudioListener.volume = backgroundSoundSlider.value;

        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);

        // Save all PlayerPrefs
        PlayerPrefs.Save();
    }

    // Back button functionality
    public void Back()
    {
        // Hide settings panel and show the pause panel
        settingsPanel.SetActive(false);  // Hide Settings Panel
        pausePanel.SetActive(true);      // Show Pause Panel
    }
}
