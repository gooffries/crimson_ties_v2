using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;          // Reference to the enemy's transform
    public Vector3 offset;            // Offset to position the health bar above the head
    public Image healthFillImage;     // Reference to the fill image of the health bar
    private float maxHealth;

    // Initialize the health bar
    public void Initialize(Transform targetTransform, float maxHealthValue, Vector3 offsetValue)
    {
        target = targetTransform;
        maxHealth = maxHealthValue;
        offset = offsetValue;
    }

    // Update the health bar's position and health
    public void UpdateHealth(float currentHealth)
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    void Update()
    {
        // Update the position of the health bar
        if (target != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = screenPosition;
        }
    }
}
