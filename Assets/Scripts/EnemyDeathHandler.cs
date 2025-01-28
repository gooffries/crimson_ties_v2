using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject splatterPrefab; // Cherry blossom splatter effect prefab
    public GameObject rewardPrefab;  // Reward object prefab
    public int rewardCount = 1;      // Number of rewards to drop
    public float rewardSpawnRadius = 0.5f; // Radius for random reward spawn

    public void HandleDeath()
    {
        // Instantiate the splatter effect
        if (splatterPrefab != null)
        {
            Vector3 splatterPosition = transform.position + new Vector3(0, 2f, 0); // Adjust Y-axis
            GameObject splatter = Instantiate(splatterPrefab, splatterPosition, Quaternion.identity);

            ParticleSystem particleSystem = splatter.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Debug.Log("Playing splatter effect.");
                particleSystem.Play();

                // Destroy splatter after effect finishes
                float effectDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constantMax;
                Destroy(splatter, effectDuration);
            }
            else
            {
                Debug.LogWarning("Splatter prefab is missing a ParticleSystem component!");
            }
        }
        else
        {
            Debug.LogWarning("Splatter prefab is not assigned!");
        }

        // Drop rewards
        if (rewardPrefab != null)
        {
            Debug.Log($"Dropping {rewardCount} rewards.");
            for (int i = 0; i < rewardCount; i++)
            {
                // Random offset for reward spawn
                Vector3 randomOffset = new Vector3(
                    Random.Range(-rewardSpawnRadius, rewardSpawnRadius),
                    0, // Spawn at ground level
                    Random.Range(-rewardSpawnRadius, rewardSpawnRadius)
                );

                Vector3 spawnPosition = transform.position + randomOffset;

                Instantiate(rewardPrefab, spawnPosition, Quaternion.identity);
                Debug.Log($"Reward spawned at: {spawnPosition}");
            }
        }
        else
        {
            Debug.LogWarning("Reward prefab is not assigned!");
        }
    }
}
