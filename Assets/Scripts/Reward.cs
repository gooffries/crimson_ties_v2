using UnityEngine;

public class Reward : MonoBehaviour
{
    public int gemValue = 1; // The value of this gem reward

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player collected {gemValue} gem(s).");

            // Add gems to the UIManager
            if (UIManager.Instance != null)
            {
                UIManager.Instance.AddGem(gemValue);
            }
            else
            {
                Debug.LogWarning("UIManager instance not found!");
            }

            // Destroy the reward object after collection
            Destroy(gameObject);
        }
    }
}
