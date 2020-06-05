using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [Header("Player info")]
    [Tooltip("Player's speed")]
    public float speed;

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

    [Header("Graphisms")]
    [Tooltip("The intensity of the sunlight")]
    public float sunIntensity;
}
