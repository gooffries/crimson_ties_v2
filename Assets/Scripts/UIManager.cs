using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI swordStrengthText; // TextMeshPro Text for sword level and strength
    public TextMeshProUGUI gemsCollectedText; // TextMeshPro Text for gems collected

    private int swordLevel = 1; // Current sword level
    private int gemsCollected = 0; // Total gems collected counter
    private int currentGems = 0; // Gems collected for the current level
    private int requiredGems = 5; // Gems required for the next level
    private float baseSwordStrength = 10f; // Base sword strength
    private float currentSwordStrength; // Sword strength based on level

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentSwordStrength = baseSwordStrength;
        UpdateSwordStrengthUI();
        UpdateGemsCollectedUI();
    }

    public void AddGem(int gemValue)
    {
        gemsCollected += gemValue; // Update total gems
        currentGems += gemValue;   // Update gems for the current level

        // Check if enough gems have been collected to level up
        if (currentGems >= requiredGems)
        {
            LevelUpSword();
        }

        // Update the UI
        UpdateSwordStrengthUI();
        UpdateGemsCollectedUI();
    }

    private void LevelUpSword()
    {
        swordLevel++; // Increase sword level
        currentGems -= requiredGems; // Carry over extra gems
        requiredGems += 5; // Increase gems required for the next level
        currentSwordStrength = baseSwordStrength * (1 + 0.05f * (swordLevel - 1)); // Increase sword strength

        Debug.Log($"Sword leveled up! Level: {swordLevel}, Strength: {currentSwordStrength}");

        // Notify WeaponController of the new sword strength
        WeaponController weaponController = FindObjectOfType<WeaponController>();
        if (weaponController != null)
        {
            weaponController.UpdateSwordStrength(currentSwordStrength);
        }
    }

    private void UpdateSwordStrengthUI()
    {
        if (swordStrengthText != null)
        {
            swordStrengthText.text = $"Level {swordLevel}: {currentGems}/{requiredGems} Gems\nStrength: {currentSwordStrength:F1}";
        }
    }

    private void UpdateGemsCollectedUI()
    {
        if (gemsCollectedText != null)
        {
            gemsCollectedText.text = $"Gems: {gemsCollected}";
        }
    }

    public float GetSwordStrength()
    {
        return currentSwordStrength;
    }
}
