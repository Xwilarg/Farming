using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Tooltip("Player speed")]
    public float speed;

    [Tooltip("Max distance where the player can place objects")]
    public int placementDist;

    [Tooltip("Max distance where the player can interract with objects")]
    public int interractDist;
}
