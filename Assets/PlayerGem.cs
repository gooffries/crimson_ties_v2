using UnityEngine;
using TMPro;

public class PlayerGem : MonoBehaviour
{
    public TextMeshProUGUI gemsCollectedText; // ✅ UI Text for gems

    private int gemsCollected = 0; // ✅ Total gems collected

    public static PlayerGem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ✅ Load stored gems from GameManager
        if (GameManager.Instance != null)
        {
            gemsCollected = GameManager.Instance.gemsCollected;
        }

        UpdateGemsCollectedUI();
    }

    public void AddGem(int gemValue)
    {
        gemsCollected += gemValue; // ✅ Increase total gems
        Debug.Log($"💎 Collected {gemValue} gems. Total: {gemsCollected}");

        // ✅ Store gems in GameManager (for persistence across levels)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.gemsCollected = gemsCollected;
        }

        UpdateGemsCollectedUI();
    }

    private void UpdateGemsCollectedUI()
    {
        if (gemsCollectedText != null)
        {
            gemsCollectedText.text = gemsCollected.ToString();
        }
        else
        {
            Debug.LogWarning("⚠️ Gems Collected UI Text is NOT assigned!");
        }
    }

    public int GetTotalGems()
    {
        return gemsCollected;
    }
}
