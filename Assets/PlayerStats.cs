using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Slider swordStrengthBar;  // Reference to the UI Slider for sword strength
    public TextMeshProUGUI gemsText; // Reference to the UI Text for displaying gems

    private float maxSwordStrength = 100f;
    private float swordStrength;
    private int gemsCollected = 0;

    void Start()
    {
        swordStrength = maxSwordStrength;
        swordStrengthBar.maxValue = maxSwordStrength;
        swordStrengthBar.value = swordStrength;
        UpdateGemsUI();
    }

    public void EnemyKilled()
    {
        // Decrease sword strength when an enemy is killed
        swordStrength -= 10f; // Adjust the value as needed
        swordStrength = Mathf.Clamp(swordStrength, 0, maxSwordStrength);
        swordStrengthBar.value = swordStrength;
    }

    public void CollectGem()
    {
        gemsCollected++;
        UpdateGemsUI();

        // Every 5 gems, increase sword strength
        if (gemsCollected % 5 == 0)
        {
            swordStrength *= 1.5f;
            swordStrength = Mathf.Clamp(swordStrength, 0, maxSwordStrength);
            swordStrengthBar.value = swordStrength;
        }
    }

    void UpdateGemsUI()
    {
        gemsText.text = gemsCollected.ToString(); // Convert int to string
    }
}
