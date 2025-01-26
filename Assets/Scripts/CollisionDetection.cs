using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wp;       // Reference to the weapon controller
    public GameObject HitParticle;   // Optional particle effect for hit feedback

    private bool hasSpawnedEffect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && wp.IsAttacking && !hasSpawnedEffect)
        {
            Debug.Log($"Enemy hit: {other.name}");

            // Access the Enemy script
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10f); // Apply damage to the enemy
            }
            else
            {
                Debug.LogWarning("Enemy script not found or enemy already destroyed.");
                return; // Stop further processing
            }

            // Trigger hit animation (optional)
            Animator anim = other.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Hit");
            }

            // Instantiate hit particle effect (optional)
            if (HitParticle != null)
            {
                Instantiate(HitParticle, other.transform.position, Quaternion.identity);
            }

            // Prevent multiple interactions
            hasSpawnedEffect = true;
            Invoke(nameof(ResetEffectSpawn), 0.5f);
        }
    }

    private void ResetEffectSpawn()
    {
        hasSpawnedEffect = false;
    }
}
