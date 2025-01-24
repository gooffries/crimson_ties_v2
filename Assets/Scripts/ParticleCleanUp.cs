using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // Destroy the particle effect after 2 seconds
    }
}
