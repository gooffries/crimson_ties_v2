using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wp;
    public GameObject HitParticle; // Assign blood effect prefab in Inspector
    public float bloodSpawnHeightOffset = 0.8f; // Adjust this to position the blood correctly

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && wp.IsAttacking)
        {
            Debug.Log($"Enemy hit: {other.name}");

            // Get the exact point of contact between the sword and the enemy
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 spawnPosition = hitPoint + new Vector3(0, bloodSpawnHeightOffset, 0); // Slightly above the hit point

            Debug.Log($"Blood should spawn at {spawnPosition}, Enemy Position: {other.transform.position}");

            // Apply damage to enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10f);
            }
            else
            {
                Debug.LogWarning($"Enemy script missing on {other.name}, skipping damage but still spawning effects.");
            }

            // Spawn a blood effect at the hit point
            if (HitParticle != null)
            {
                GameObject blood = Instantiate(HitParticle, spawnPosition, Quaternion.identity);
                blood.transform.SetParent(other.transform); // Attach to enemy so it moves with them
                Debug.Log($"Blood effect spawned at {spawnPosition} and attached to {other.name}");
            }
        }
    }
}
