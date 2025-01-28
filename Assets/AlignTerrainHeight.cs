using UnityEngine;

public class AlignTerrainHeight : MonoBehaviour
{
    public GameObject defaultTerrain; // Assign your Unity Terrain
    public GameObject customTerrain; // Assign your Custom Terrain (FBX)

    void Start()
    {
        // Get the Y position of the default terrain
        float defaultTerrainHeight = defaultTerrain.transform.position.y;

        // Match the custom terrain's Y position to the default terrain's height
        Vector3 customPosition = customTerrain.transform.position;
        customPosition.y = defaultTerrainHeight;
        customTerrain.transform.position = customPosition;
    }
}
