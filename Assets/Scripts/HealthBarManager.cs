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

            // Initialize the health bar
            healthBar.Initialize(enemy.transform, maxHealth);
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
