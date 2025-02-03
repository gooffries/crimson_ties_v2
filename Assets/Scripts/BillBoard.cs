using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform cam; // Reference to the main camera transform

    void Start()
    {
        cam = Camera.main.transform; // ✅ Automatically find the main camera
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            // ✅ Rotate only on the Y-axis (prevents weird tilting)
            Vector3 direction = cam.position - transform.position;
            direction.y = 0; // ✅ Remove vertical rotation
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.LogError("❌ ERROR: Camera reference is missing in BillBoard script.");
        }
    }
}
