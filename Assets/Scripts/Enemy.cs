using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private HealthBarManager healthBarManager;

    void Start()
    {
        currentHealth = maxHealth;

        // Use the new method to find the HealthBarManager
        healthBarManager = Object.FindFirstObjectByType<HealthBarManager>();

        if (healthBarManager == null)
        {
            Debug.LogError("HealthBarManager not found in the scene!");
            return;
        }

        // Add health bar to UI
        healthBarManager.AddHealthBar(gameObject, maxHealth);
        Debug.Log("HealthBarManager added!");
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        // Update the health bar
        healthBarManager.UpdateHealthBar(gameObject, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Notify the HealthBarManager to remove the health bar
        healthBarManager.RemoveHealthBar(gameObject);

        // Call EnemyDeathHandler to handle death effects
        EnemyDeathHandler deathHandler = GetComponent<EnemyDeathHandler>();
        if (deathHandler != null)
        {
            deathHandler.HandleDeath();
        }
        else
        {
            Debug.LogWarning("EnemyDeathHandler not found on this enemy!");
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }


}
