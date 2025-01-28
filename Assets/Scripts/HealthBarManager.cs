using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public GameObject healthBarPrefab; // Prefab for the health bar
    public Canvas canvas;              // UI Canvas for health bars
    private Dictionary<GameObject, HealthBar> healthBars = new Dictionary<GameObject, HealthBar>();

    public void AddHealthBar(GameObject enemy, float maxHealth)
    {
        Debug.Log($"Adding health bar for: {enemy.name}");
        if (!healthBars.ContainsKey(enemy))
        {
            if (healthBarPrefab == null)
            {
                Debug.LogError("HealthBarPrefab is not assigned in HealthBarManager!");
                return;
            }

            if (canvas == null)
            {
                Debug.LogError("Canvas is not assigned in HealthBarManager!");
                return;
            }

            // Instantiate the health bar
            GameObject healthBarObject = Instantiate(healthBarPrefab, canvas.transform);
            HealthBar healthBar = healthBarObject.GetComponent<HealthBar>();

            if (healthBar == null)
            {
                Debug.LogError("HealthBar script is missing from HealthBarPrefab!");
                return;
            }

            // Calculate enemy height for dynamic offset
            float height = 2.0f; // Default offset
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                height = enemyCollider.bounds.size.y + 0.5f; // Add slight padding above the head
            }

            // Initialize the health bar
            Vector3 offset = new Vector3(0, height, 0); // Offset above the enemy's head
            healthBar.Initialize(enemy.transform, maxHealth, offset);
            healthBars[enemy] = healthBar;
            Debug.Log($"Health bar successfully added for: {enemy.name}");
        }
    }

    public void UpdateHealthBar(GameObject enemy, float currentHealth)
    {
        if (healthBars.ContainsKey(enemy))
        {
            healthBars[enemy].UpdateHealth(currentHealth);
        }
    }

    public void RemoveHealthBar(GameObject enemy)
    {
        if (healthBars.ContainsKey(enemy))
        {
            Destroy(healthBars[enemy].gameObject); // Destroy the health bar UI
            healthBars.Remove(enemy);             // Remove it from the dictionary
        }
    }
}
