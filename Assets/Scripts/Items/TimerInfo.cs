using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TimerInfo", fileName = "TimerInfo")]
public class TimerInfo : ScriptableObject
{
    [Tooltip("Time before a wet dirt loose all its water")]
    public float dirtWetTimer;
}
