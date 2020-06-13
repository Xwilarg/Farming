using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlantInfo", fileName = "PlantInfo")]
public class PlantInfo : ScriptableObject
{
    [Tooltip("All the different progression steps of the plant.")]
    public Mesh States;

    [Tooltip("Time before the plant reach its next state")]
    public float TimeBetweenStates;

    [Tooltip("Does the plant need to be watered to grow")]
    public bool doesNeedWater;
}
