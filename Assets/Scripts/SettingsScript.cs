using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SettingsScript : MonoBehaviour
{
    // UI Elements
    public GameObject settingsPanel;  // Settings Panel
    public GameObject pausePanel;     // Pause Panel

    public Slider backgroundSoundSlider;
    public Slider soundEffectsSlider;
    //public Dropdown resolutionDropdown;

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    //private Resolution[] resolutions;
    private int selectedDifficulty;

    void Start()
    {
        // Initialize settings (load from PlayerPrefs if available)
        backgroundSoundSlider.value = PlayerPrefs.GetFloat("BackgroundSound", 1f);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffects", 1f);
        AudioListener.volume = backgroundSoundSlider.value;

        selectedDifficulty = PlayerPrefs.GetInt("DifficultyLevel", 1); // Default: Normal
        UpdateDifficultyUI();

        // Load resolutions for dropdown
        /*resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        */

        // Button click listeners for difficulty level
        easyButton.onClick.AddListener(() => SetDifficulty(0));
        normalButton.onClick.AddListener(() => SetDifficulty(1));
        hardButton.onClick.AddListener(() => SetDifficulty(2));
    }

    // Handle difficulty button clicks
    void SetDifficulty(int difficulty)
    {
        selectedDifficulty = difficulty;
        PlayerPrefs.SetInt("DifficultyLevel", difficulty);
        PlayerPrefs.Save();
        UpdateDifficultyUI();
    }

    // Update UI for difficulty level buttons
    void UpdateDifficultyUI()
    {
        easyButton.image.color = (selectedDifficulty == 0) ? Color.green : Color.white;
        normalButton.image.color = (selectedDifficulty == 1) ? Color.green : Color.white;
        hardButton.image.color = (selectedDifficulty == 2) ? Color.green : Color.white;
    }

    // Apply settings (background sound, sound effects, resolution)
    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("BackgroundSound", backgroundSoundSlider.value);
        AudioListener.volume = backgroundSoundSlider.value;

        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);

        /*int resolutionIndex = resolutionDropdown.value;
        Resolution selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        */

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
