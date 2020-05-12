using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GeneratorInfo", fileName = "GeneratorInfo")]
public class GeneratorInfo : ScriptableObject
{
    public int Power;

    public static GeneratorInfo Info;

    private GeneratorInfo()
    {
        Info = this;
    }
}
