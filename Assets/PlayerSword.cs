using UnityEngine;
using UnityEngine.UI;

public class SwordStrength : MonoBehaviour
{
    public float maxStrength = 100f; // ✅ Maximum sword strength
    private float currentStrength;
    public Slider swordStrengthBar; // ✅ Assign this in Unity UI

    void Start()
    {
        // ✅ Load stored sword strength from GameManager or start with max
        if (GameManager.Instance != null)
        {
            currentStrength = GameManager.Instance.swordStrength;
            if (currentStrength <= 0) // Prevent broken sword on respawn
            {
                currentStrength = maxStrength;
            }
        }
        else
        {
            currentStrength = maxStrength;
        }

        UpdateSwordStrengthBar();
    }

    void Update()
    {
        Debug.Log("🗡️ Sword Strength: " + currentStrength);
    }

    public void ReduceStrength(float damage)
    {
        currentStrength = Mathf.Clamp(currentStrength - damage, 0, maxStrength);
        Debug.Log($"⚔️ Sword took {damage} damage. Current strength: {currentStrength}");

        UpdateSwordStrengthBar();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.swordStrength = currentStrength; // ✅ Store strength before scene transition
        }

        if (currentStrength <= 0)
        {
            BreakSword();
        }
    }

    private void UpdateSwordStrengthBar()
    {
        if (swordStrengthBar != null)
        {
            swordStrengthBar.value = currentStrength / maxStrength;
        }
        else
        {
            Debug.LogWarning("⚠️ Sword Strength Bar not assigned!");
        }
    }

    private void BreakSword()
    {
        Debug.Log("🚨 Sword is broken! You need to repair or upgrade it!");
        // ✅ Add logic for broken sword (disable attacks, show UI, etc.)
    }
}
