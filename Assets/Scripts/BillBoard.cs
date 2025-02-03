using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam; // Reference to the main camera transform

    void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.forward);
        }
        else
        {
            Debug.LogError("Camera reference is missing in BillBoard script.");
        }
    }
}
