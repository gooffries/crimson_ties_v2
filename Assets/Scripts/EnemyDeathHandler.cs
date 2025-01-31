using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject splatterPrefab; // Cherry blossom splatter effect prefab
    public GameObject rewardPrefab;  // Reward object prefab
    public int rewardCount = 1;      // Number of rewards to drop
    public float rewardSpawnRadius = 0.5f; // Radius for random reward spawn

    private Vector3 deathPosition;

    public void HandleDeath()
    {
        deathPosition = transform.position; // Record enemy's final position
        Debug.Log($"📍 Enemy's final position recorded: {deathPosition}");

        HandleSplatterEffect(); // ✅ Spawn effect first
        DropRewards(); // ✅ Drop rewards

        Debug.Log("💀 Destroying enemy...");
        Destroy(gameObject, 2f); // ✅ Destroy enemy after everything is done
    }

    private void HandleSplatterEffect()
    {
        if (splatterPrefab != null)
        {
            Vector3 splatterPosition = deathPosition + new Vector3(0, 8f, 0); // Adjust Y-axis
            GameObject splatter = Instantiate(splatterPrefab, splatterPosition, Quaternion.identity);

            ParticleSystem particleSystem = splatter.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Debug.Log("💥 Playing splatter effect.");
                particleSystem.Play();

                // Destroy splatter after effect finishes
                float effectDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constantMax;
                Destroy(splatter, effectDuration);
            }
            else
            {
                Debug.LogWarning("⚠️ Splatter prefab is missing a ParticleSystem component!");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Splatter prefab is not assigned!");
        }
    }

    private void DropRewards()
    {
        if (rewardPrefab != null)
        {
            Debug.Log($"🎁 Dropping {rewardCount} rewards.");
            for (int i = 0; i < rewardCount; i++)
            {
                Vector3 spawnPosition = deathPosition + new Vector3(0, 5f, 0);

                // ✅ Ensure rewards are instantiated in the root of the scene
                GameObject reward = Instantiate(rewardPrefab, spawnPosition, Quaternion.identity);
                reward.transform.SetParent(null); // ✅ Prevent reward from being deleted when the enemy is destroyed

                Debug.Log($"🎁 Reward spawned at: {spawnPosition}");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Reward prefab is not assigned!");
        }
    }
}
