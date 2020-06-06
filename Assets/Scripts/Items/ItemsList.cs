using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemsList", fileName = "ItemsList")]
public class ItemsList : ScriptableObject
{
    [Header("Sprites image for UI")]
    [SerializeField]
    private Sprite GeneratorImg;
    [SerializeField]
    private Sprite BasicPlantImg;
    [SerializeField]
    private Sprite ScytheImg;
    [SerializeField]
    private Sprite SpadeImg;
    [SerializeField]
    private Sprite WaterImg;
    [SerializeField]
    private Sprite GunEnergyImg;
    [SerializeField]
    private Sprite GunMechanicImg;
    [SerializeField]
    private Sprite ShotgunImg;
    [SerializeField]
    private Sprite EnemyImg;

    [Header("Prefab GameObject to be put ingame")]
    [SerializeField]
    private GameObject GeneratorGo;
    [SerializeField]
    private GameObject BasicPlantGo;
    [SerializeField]
    private GameObject EnemyGo;

    public static ItemsList Items;

    private ItemsList()
    {
        Items = this;
    }

    /// <summary>
    /// Somehow the items defined in the editor are null when I get them from the ctor so I need to do another function was is called in the Awake of a MonoBehaviour object
    /// </summary>
    public void Init()
    {
        AllItems = new Dictionary<ItemID, Item>
        {
            { ItemID.Generator, new Item(ItemID.Generator, "Generator", "Generate a livable space around you", new[] { TileType.Grass, TileType.Sand }, GeneratorImg, GeneratorGo, new GeneratorPower()) },
            { ItemID.BasicPlant, new Item(ItemID.BasicPlant, "Basic Plant", "Maybe a cute flower will grow?", new[] { TileType.Dirt, TileType.WetDirt }, BasicPlantImg, BasicPlantGo, null) },
            { ItemID.Scythe, new Item(ItemID.Scythe, "Scythe", "To cut down plants and annoying salesman", new TileType[0], ScytheImg, null, null) },
            { ItemID.Spade, new Item(ItemID.Spade, "Spade", "Plow the earth to make a comfy place for your plants", new[] { TileType.Grass }, SpadeImg, null, new SpadePower()) },
            { ItemID.Water, new Item(ItemID.Water, "Water", "The healthiest of all the drinks", new[] { TileType.Dirt, TileType.WetDirt }, WaterImg, null, new WaterPower()) },
            { ItemID.GunEnergy, new Item(ItemID.GunEnergy, "Gun (Energy)", "A basic handgun, especially strong against mechanic shields", new[] { TileType.Special }, GunEnergyImg, null, new EnergyGunPower()) },
            { ItemID.GunMechanic, new Item(ItemID.GunMechanic, "Gun (Mechanic)", "A basic handgun, especially strong against energy shields", new[] { TileType.Special }, GunMechanicImg, null, new MechanicalGunPower()) },
            { ItemID.Shotgun, new Item(ItemID.Shotgun, "Shotgun", "An alternative way to open doors", new[] { TileType.Special }, ShotgunImg, null, new ShotgunPower()) },
            { ItemID.DemonicCat, new Item(ItemID.DemonicCat, "Demonic Cat", "Devilishly cute", new[] { TileType.Sand }, EnemyImg, EnemyGo, null) }
        };
    }

    public Dictionary<ItemID, Item> AllItems;
}
