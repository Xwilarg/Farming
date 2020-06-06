using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Player info")]
    [Tooltip("Player speed")]
    public float speed;

    [Tooltip("Max distance where the player can place objects")]
    public int placementDist;

    [Header("Controls")]
    [Tooltip("Mouse default sensibivity on the X axis")]
    public float xSensibivity;

    [Tooltip("Mouse default sensibility on the Y axis")]
    public float ySensibility;

    [Tooltip("Invert X axis")]
    public bool invertXAxis;

    [Tooltip("Invert Y axis")]
    public bool invertYAxis;

    [Tooltip("Default key to go forward")]
    public KeyCode forwardKey;

    [Tooltip("Default key to go backward")]
    public KeyCode backwardKey;

    [Tooltip("Default key to go left")]
    public KeyCode leftKey;

    [Tooltip("Default key to go right")]
    public KeyCode rightKey;

    [Tooltip("Default key to enable/disable object placement")]
    public KeyCode placementKey;

    [Tooltip("Default key to toggle inventory")]
    public KeyCode inventoryKey;

    [Header("Graphisms")]
    [Tooltip("The intensity of the sunlight")]
    public float sunIntensity;
}
