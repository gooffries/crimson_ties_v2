using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;              // Enemy's transform
    public Vector3 offset = new Vector3(0, 8f, 0); // Offset for positioning
    public Image healthFillImage;         // UI fill image for the health bar
    public float maxHealth;              // Private to prevent unintended modifications

    // Initialize the health bar

    void Start()
    {
        if (healthFillImage == null)
        {
            Debug.LogWarning("⚠️ HealthFillImage was NULL at Start. Checking UI elements...");
            healthFillImage = GetComponentInChildren<Image>(); // Try to auto-assign
        }

        if (healthFillImage != null)
        {
            Debug.Log($"✅ HealthFillImage successfully assigned: {healthFillImage.name}");
        }
        else
        {
            Debug.LogError("❌ CRITICAL: HealthFillImage is still NULL! Check prefab assignment.");
        }
    }


    public void Initialize(Transform targetTransform, float maxHealthValue, Vector3 offsetValue)
    {
        target = targetTransform;
        maxHealth = maxHealthValue;

        // ✅ Dynamically adjust offset based on model height
        Renderer renderer = target.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            float height = renderer.bounds.size.y; // Get model height
            offset = new Vector3(0, height * 2f, 0); // Slightly above head
            Debug.Log($"📍 HealthBar offset set to {offset.y} based on enemy height: {height}");
        }
        else
        {
            offset = offsetValue; // Default value if no renderer found
        }
    }




    public void UpdateHealth(float currentHealth)
    {
        if (healthFillImage == null)
        {
            Debug.LogError("❌ HealthFillImage is NULL in UpdateHealth()! UI won’t update.");
            return;
        }

        float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);

        Debug.Log($"✅ Updating health bar: {currentHealth}/{maxHealth} → {healthPercentage * 100}%");

        healthFillImage.fillAmount = healthPercentage; // ✅ Directly update fill amount

        // ✅ Force UI Refresh
        healthFillImage.enabled = false;
        healthFillImage.enabled = true;

        // ✅ If using a Slider component, update its value
        Slider slider = GetComponentInChildren<Slider>();

        if (slider != null)
        {
            slider.minValue = 0f;  // Ensure the slider starts at 0
            slider.maxValue = 1f;  // Ensure the slider maxes at 1
            slider.value = healthPercentage; // ✅ Update using normalized value
            Debug.Log($"✅ Updating slider value: {slider.value}");
        }
        else
        {
            Debug.LogWarning("⚠️ Slider component not found inside HealthBar!");
        }
    }




    void Update()
    {
        if (target != null)
        {
            // Keep the bar positioned above the enemy
            transform.position = target.position + offset;

            // Make the health bar always face the camera
            if (Camera.main != null)
            {
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0); // Flip to ensure it faces correctly
            }
            else
            {
                Debug.LogWarning("Main Camera not found!");
            }

            // Fix any unwanted scaling issues
            transform.localScale = Vector3.one;
        }
    }
}
