using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;          // Enemy's transform
    public Vector3 offset;            // Offset for positioning the health bar
    public Image healthFillImage;     // UI fill image for the health bar
    private float maxHealth;

    // Initialize the health bar
    public void Initialize(Transform targetTransform, float maxHealthValue, Vector3 offsetValue)
    {
        target = targetTransform;
        maxHealth = maxHealthValue;
        offset = offsetValue;
    }

    // Update the health bar's health value
    public void UpdateHealth(float currentHealth)
    {
        if (healthFillImage != null)
        {
            float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);
            healthFillImage.fillAmount = healthPercentage;
        }
    }

    void Update()
    {
        // Ensure the target exists and Camera.main is not null
        if (target != null && Camera.main != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position + offset);
            GetComponent<RectTransform>().position = screenPosition; // Use RectTransform for UI positioning
        }
    }
}
