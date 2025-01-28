using UnityEngine;
using TMPro; // For TextMeshPro

public class GameTimer : MonoBehaviour
{
    public float startTime = 300f; // Timer starts at 5 minutes (300 seconds)
    private float currentTime;
    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = startTime; // Initialize the timer
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrease time
            UpdateTimerUI(); // Update the text on the screen
        }
        else
        {
            currentTime = 0; // Stop the timer at 0
            TimerEnded();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60); // Get minutes
        int seconds = Mathf.FloorToInt(currentTime % 60); // Get seconds
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Format MM:SS
    }

    void TimerEnded()
    {
        Debug.Log("Time's up!");
        // Add logic for what happens when the timer ends
    }
}

