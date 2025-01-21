using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemCollection : MonoBehaviour
{
    private int Gem = 0;

    public TextMeshProUGUI gemText;

    private void OnTriggerEnter(Collider other)
    {
        Gem++;
        gemText.text = "Gems:" + Gem.ToString();
        Debug.Log(Gem);
        Destroy(other.gameObject);
    }
}
