using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DayNightInfo", fileName = "DayNightInfo")]
public class DayNightInfo : ScriptableObject
{
    [Tooltip("Length of the day in minute")]
    public float dayLength;

    [Tooltip("Length of the night in minute")]
    public float nightLength;
}
