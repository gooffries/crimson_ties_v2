using UnityEngine;

public class MatchTerrainHeight : MonoBehaviour
{
    public Terrain defaultTerrain; // Assign your Unity terrain
    public GameObject customTerrain; // Assign your custom terrain (FBX)

    void Start()
    {
        if (defaultTerrain == null || customTerrain == null)
        {
            Debug.LogError("Default Terrain or Custom Terrain is not assigned!");
            return;
        }

        // Get the height range of the Unity terrain
        float unityHeightRange = defaultTerrain.terrainData.size.y;

        // Get the current Y-scale of the custom terrain
        Vector3 customScale = customTerrain.transform.localScale;

        // Calculate the actual height range of the custom terrain
        Renderer customRenderer = customTerrain.GetComponent<Renderer>();
        if (customRenderer == null)
        {
            Debug.LogError("Custom Terrain does not have a Renderer component!");
            return;
        }
        float customHeightRange = customRenderer.bounds.size.y;

        // Adjust the Y-scale of the custom terrain to match the Unity terrain's height range
        customScale.y = customScale.y * (unityHeightRange / customHeightRange);
        customTerrain.transform.localScale = customScale;

        Debug.Log($"Custom Terrain scaled to match Unity Terrain height. New Scale: {customTerrain.transform.localScale}");
    }
}

