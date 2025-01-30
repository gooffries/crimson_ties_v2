using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar; // ✅ Assign this in Unity

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        Debug.Log($"💔 Player took {damage} damage. Current health: {currentHealth}");

        UpdateHealthBar();

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
            Debug.LogWarning("⚠ Player HealthBar not assigned!");
        }
    }

    private void Die()
    {
        Debug.Log("☠ Player has died!");
        // ✅ Add game over logic here (disable controls, show UI, etc.)
    }
}
