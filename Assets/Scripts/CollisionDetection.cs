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
            Debug.Log($"⚔️ Enemy hit: {other.name}");

            // ✅ Get the exact point of contact
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 spawnPosition = hitPoint + Vector3.up * 1.2f; // Adjust if needed

            // ✅ Try to get GuardAI script first
            GuardAI guard = other.GetComponent<GuardAI>();
            Enemy enemy = other.GetComponent<Enemy>();

            if (guard != null)
            {
                guard.TakeDamage(10f); // ✅ Damage for Guards
                Debug.Log($"⚔️ {other.name} (Guard) took damage!");
            }
            else if (enemy != null)
            {
                enemy.TakeDamage(10f); // ✅ Damage for Enemies
                Debug.Log($"⚔️ {other.name} (Enemy) took damage!");
            }
            else
            {
                Debug.LogWarning($"⚠️ No valid damage script found on {other.name}, skipping damage.");
            }

            // ✅ Spawn a blood effect
            if (HitParticle != null)
            {
                GameObject blood = Instantiate(HitParticle, spawnPosition, Quaternion.identity);
                blood.transform.SetParent(other.transform);
                Debug.Log($"💥 Blood effect spawned at {spawnPosition} for {other.name}");
            }
        }
    }

}
