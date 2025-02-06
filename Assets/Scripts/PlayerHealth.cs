using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar; // ✅ Assign this in Unity

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void Start()
    {
        // ✅ Load stored health from GameManager or start with maxHealth
        if (GameManager.Instance != null)
        {
            currentHealth = GameManager.Instance.playerHealth;
            if (currentHealth <= 0) // Prevent negative health on respawn
            {
                currentHealth = maxHealth;
            }
        }
        else
        {
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
    }

    void Update()
    {
        Debug.Log("the player's curent health is: " + currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log($"💔 Player took {damage} damage. Current health: {currentHealth}");

        UpdateHealthBar();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerHealth = currentHealth; // ✅ Store health before scene transition
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("⚠️ Player HealthBar not assigned!");
        }
    }

    private void Die()
    {
        Debug.Log("☠️ Player has died!");
        // ✅ Add game over logic here (disable controls, show UI, etc.)
    }
}