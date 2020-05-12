using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GenerationInfo", fileName = "GenerationInfo")]
public class GenerationInfo : ScriptableObject
{
    [Tooltip("Number of tile in a chunk, must be an odd number")]
    public int ChunkSize;
}
