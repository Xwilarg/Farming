using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySpawnInfo", fileName = "EnemySpawnInfo")]
public class EnemySpawnInfo : ScriptableObject
{
    [Tooltip("Maximum number of enemies that can be on the world at once")]
    public int maxEnnemiesAtOnce;

    [Tooltip("Time between 2 spawns")]
    public int timeBetweenSpawn;

    [Tooltip("Prefab of the enemy")]
    public GameObject prefabEnemy;

    [Tooltip("Distance between the player and where the enemy spawn")]
    public int distanceSpawn;
}
