using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            // Add death logic here
        }
    }
}

