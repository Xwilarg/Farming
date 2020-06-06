using UnityEngine;

public class ItemsInit : MonoBehaviour
{
    [Header("Item List Scriptable Object")]
    [SerializeField]
    private ItemsList items;

    [Header("All Individial Scriptable Object")]
    public GeneratorInfo mainGeneratorInfo;
    public WeaponInfo energyGun, mechanicalGun, shotgun;

    public static ItemsInit S;

    private void Awake()
    {
        S = this;
        items.Init();
    }
}
