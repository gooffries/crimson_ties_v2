using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject splatterPrefab; // Cherry blossom splatter effect prefab
    public GameObject rewardPrefab;  // Reward object prefab
    public int rewardCount = 1;      // Number of rewards to drop

    public void HandleDeath()
    {
        // Instantiate the splatter effect at the enemy's position
        if (splatterPrefab != null)
        {
            Vector3 splatterPosition = transform.position + new Vector3(0, 2f, 0); // Adjust Y-axis
            GameObject splatter = Instantiate(splatterPrefab, splatterPosition, Quaternion.identity);

            ParticleSystem particleSystem = splatter.GetComponent<ParticleSystem>();

            if (particleSystem != null)
            {
                particleSystem.Play(); // Play the particle system
                Destroy(splatter, particleSystem.main.duration); // Destroy the splatter object after the effect finishes
            }
        }
        else
        {
            Debug.LogWarning("Splatter prefab is not assigned!");
        }

        // Drop rewards
        if (rewardPrefab != null)
        {
            for (int i = 0; i < rewardCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    1f, // Adjust height if necessary
                    Random.Range(-0.5f, 0.5f)
                );

                Instantiate(rewardPrefab, transform.position + randomOffset, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("Reward prefab is not assigned!");
        }
    }
}
