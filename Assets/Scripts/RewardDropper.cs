using UnityEngine;

public class RewardDropper : MonoBehaviour
{
    public GameObject rewardPrefab; // The collectible reward prefab
    public GameObject splatterPrefab; // The cherry blossom splatter prefab
    public int rewardCount = 1; // Number of rewards to drop

    public void DropRewardAndSplatter(Vector3 position)
    {
        // Spawn the cherry blossom splatter
        if (splatterPrefab != null)
        {
            Debug.Log("Spawning cherry blossom splatter"); // Debug log
            Instantiate(splatterPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("SplatterPrefab is not assigned!");
        }

        // Spawn the reward(s)
        if (rewardPrefab != null)
        {
            Debug.Log("Spawning reward prefab"); // Debug log
            Instantiate(rewardPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("RewardPrefab is not assigned!");
        }
    }

}
