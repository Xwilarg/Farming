using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySpawnInfo", fileName = "EnemySpawnInfo")]
public class EnemySpawnInfo : ScriptableObject
{
    [Tooltip("Number of ennemies per waves")]
    public int ennemiesPerWaves;

    [Tooltip("Time between 2 waves")]
    public int timeBetweenWaves;

    [Tooltip("Prefab of the enemy")]
    public GameObject prefabEnemy;

    [Tooltip("Distance between the player and where the enemy spawn")]
    public int distanceSpawn;
}
