using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public GameObject healthBarPrefab; // Prefab for the health bar UI
    public Canvas canvas;              // UI Canvas for health bars
    private Dictionary<GameObject, HealthBar> healthBars = new Dictionary<GameObject, HealthBar>();

    void Start()
    {
        StartCoroutine(DelayedSetup()); // Ensures all enemies are spawned before adding health bars
    }

    private System.Collections.IEnumerator DelayedSetup()
    {
        yield return new WaitForSeconds(1f); // Wait to ensure enemies exist
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            AddHealthBar(enemy.gameObject, enemy.maxHealth);
        }
    }

    /// <summary>
    /// Adds a health bar to an enemy if it doesn't already have one.
    /// </summary>
    public void AddHealthBar(GameObject enemy, float maxHealth)
    {
        if (enemy == null) return;

        // ✅ Check if the enemy already has a health bar
        if (healthBars.ContainsKey(enemy))
        {
            Debug.LogWarning($"⚠️ Health bar already exists for {enemy.name}, skipping creation.");
            return;
        }

        if (healthBarPrefab == null)
        {
            Debug.LogError("❌ HealthBarPrefab is NOT assigned in HealthBarManager!");
            return;
        }

        // ✅ Instantiate the health bar and attach it to the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, canvas.transform);
        HealthBar healthBar = healthBarObject.GetComponent<HealthBar>();

        if (healthBar == null)
        {
            Debug.LogError("❌ CRITICAL: HealthBar script is missing from instantiated HealthBarPrefab!");
            return;
        }

        healthBar.Initialize(enemy.transform, maxHealth, new Vector3(0, 8f, 0));

        // ✅ Store the health bar in the dictionary
        healthBars[enemy] = healthBar;
        Debug.Log($"✅ Successfully added Health Bar for {enemy.name}");
    }


    /// <summary>
    /// Updates the health bar of an enemy.
    /// </summary>
    public void UpdateHealthBar(GameObject enemy, float currentHealth)
    {
        if (enemy == null) return;

        if (healthBars.ContainsKey(enemy))
        {
            Debug.Log($"🩸 Updating {enemy.name}'s health bar: {currentHealth}/{healthBars[enemy].maxHealth}");
            healthBars[enemy].UpdateHealth(currentHealth);
        }
        else
        {
            Debug.LogWarning($"⚠️ No health bar found for {enemy.name}! Running emergency AddHealthBar...");

            // ✅ If health bar is missing, attempt to re-add it
            AddHealthBar(enemy, currentHealth);
        }
    }

    /// <summary>
    /// Removes a health bar when an enemy dies.
    /// </summary>
    public void RemoveHealthBar(GameObject enemy)
    {
        if (enemy == null) return;

        if (healthBars.ContainsKey(enemy))
        {
            Debug.Log($"🗑 Removing Health Bar for {enemy.name}");
            Destroy(healthBars[enemy].gameObject); // ✅ Destroy the UI element
            healthBars.Remove(enemy);
        }
    }
}
