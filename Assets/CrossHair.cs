using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Canvas pauseMenuCanvas; // Reference to the pause menu Canvas
    private RectTransform crosshairRectTransform;

    void Start()
    {
        crosshairRectTransform = GetComponent<RectTransform>(); // Get the RectTransform of the crosshair
    }

    void Update()
    {
        // Check if the game is paused
        if (GameManager.Instance != null && GameManager.Instance.IsGamePaused)
        {
            SetCrosshairBehindUI(); // Move crosshair behind the UI
        }
        else
        {
            SetCrosshairOnTop(); // Bring crosshair back in front of the UI
        }
    }

    // Move the crosshair behind UI elements (paused)
    private void SetCrosshairBehindUI()
    {
        if (crosshairRectTransform != null && pauseMenuCanvas != null)
        {
            // Set the Z position behind the canvas
            crosshairRectTransform.SetSiblingIndex(0); // Move to the bottom of the hierarchy
        }
    }

    // Bring the crosshair to the front (not paused)
    private void SetCrosshairOnTop()
    {
        if (crosshairRectTransform != null && pauseMenuCanvas != null)
        {
            // Set the Z position in front of the canvas
            crosshairRectTransform.SetSiblingIndex(1); // Move above the background canvas
        }
    }
}

