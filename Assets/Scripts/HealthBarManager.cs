using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public GameObject healthBarPrefab; // Prefab for the health bar
    public Canvas canvas;              // UI Canvas for health bars
    private Dictionary<GameObject, HealthBar> healthBars = new Dictionary<GameObject, HealthBar>();

    void Start()
    {
        StartCoroutine(DelayedSetup()); // Ensures all enemies are spawned before adding health bars
    }

    private System.Collections.IEnumerator DelayedSetup()

    {
        yield return new WaitForSeconds(1f); // Wait a bit to make sure all enemies exist
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            AddHealthBar(enemy.gameObject, enemy.maxHealth);
        }
    }



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

            // Fix scaling issue (ensures health bar is visible)
            healthBarObject.transform.localScale = Vector3.one * 0.5f; // Adjust size here if needed

            // Calculate enemy height for dynamic offset
            float height = 1.5f; // Default offset (slightly lower)
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                height = enemyCollider.bounds.size.y + 0.3f; // Lowered height so it’s not too high
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
