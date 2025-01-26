using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;  // Main health slider
    public Transform target;    // The enemy this health bar is tracking
    public Vector3 offset = new Vector3(0, 2, 0); // Offset above the enemy's head

    private float lerpSpeed = 0.1f; // Speed for easing effect

    public void Initialize(Transform targetTransform, float maxHealth)
    {
        target = targetTransform; // Assign the enemy to track
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateHealth(float currentHealth)
    {
        healthSlider.value = Mathf.Clamp(currentHealth, 0, healthSlider.maxValue);
    }

    private void LateUpdate()
    {
        // Follow the enemy's position
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
        else
        {
            // Destroy the health bar if the target no longer exists
            Destroy(gameObject);
        }
    }
}
