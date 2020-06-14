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
    [SerializeField]
    private Sprite SniperImg;
    [SerializeField]
    private Sprite GunBounceImg;
    [SerializeField]
    private Sprite ChestImg;
    [SerializeField]
    private Sprite BasicPlantSeedImg;

    [Header("Prefab GameObject to be put ingame")]
    [SerializeField]
    private GameObject GeneratorGo;
    [SerializeField]
    private GameObject BasicPlantGo;
    [SerializeField]
    private GameObject EnemyGo;
    [SerializeField]
    private GameObject ChestGo;

    [Header("Weapon scriptable objects")]
    [SerializeField]
    private WeaponInfo GunEnergyInfo;
    [SerializeField]
    private WeaponInfo GunMechanicalInfo;
    [SerializeField]
    private WeaponInfo ShotgunInfo;
    [SerializeField]
    private WeaponInfo SniperInfo;
    [SerializeField]
    private WeaponInfo GunBounceInfo;

    [Header("Plant scriptable objects")]
    [SerializeField]
    private PlantInfo BasicPlantInfo;

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
            { ItemID.Generator, new Item(ItemID.Generator, "Generator", "Generate a livable space around you", new[] { TileType.Grass, TileType.Sand }, GeneratorImg, GeneratorGo, typeof(GeneratorPower)) },
            { ItemID.BasicPlant, new Item(ItemID.BasicPlant, "Basic Plant", "Just a basic plant, you can probably sell it", new TileType[0], BasicPlantImg, null, typeof(EmptyPower)) },
            { ItemID.Scythe, new Item(ItemID.Scythe, "Scythe", "To cut down plants and annoying salesman", new [] { TileType.Dirt, TileType.WetDirt }, ScytheImg, null, typeof(ScythePower)) },
            { ItemID.Spade, new Item(ItemID.Spade, "Spade", "Plow the earth to make a comfy place for your plants", new[] { TileType.Grass }, SpadeImg, null, typeof(SpadePower)) },
            { ItemID.Water, new Item(ItemID.Water, "Water", "The healthiest of all the drinks", new[] { TileType.Dirt, TileType.WetDirt }, WaterImg, null, typeof(WaterPower)) },
            { ItemID.GunEnergy, new Item(ItemID.GunEnergy, "Gun (Energy)", "A basic handgun, especially strong against mechanic shields", new[] { TileType.Special }, GunEnergyImg, null, typeof(GunPower), GunEnergyInfo) },
            { ItemID.GunMechanic, new Item(ItemID.GunMechanic, "Gun (Mechanic)", "A basic handgun, especially strong against energy shields", new[] { TileType.Special }, GunMechanicImg, null, typeof(GunPower), GunMechanicalInfo) },
            { ItemID.Shotgun, new Item(ItemID.Shotgun, "Shotgun", "An alternative way to open doors", new[] { TileType.Special }, ShotgunImg, null, typeof(GunPower), ShotgunInfo) },
            { ItemID.DemonicCat, new Item(ItemID.DemonicCat, "Demonic Cat", "Devilishly cute", new[] { TileType.Sand }, EnemyImg, EnemyGo, typeof(EmptyPower)) },
            { ItemID.Sniper, new Item(ItemID.Sniper, "Sniper", "To shoot enemies from the safety of your home", new[] { TileType.Special }, SniperImg, null, typeof(GunPower), SniperInfo) },
            { ItemID.GunBounce, new Item(ItemID.GunBounce, "Bounce Gun", "It's like a regular gun but more annoying", new[] { TileType.Special }, GunBounceImg, null, typeof(GunPower), GunBounceInfo) },
            { ItemID.Chest, new Item(ItemID.Chest, "Chest", "How can anyone resist this beautiful chest", new[] { TileType.All }, ChestImg, ChestGo, typeof(EmptyPower)) },
            { ItemID.BasicPlantSeed, new Item(ItemID.BasicPlantSeed, "Basic Plant Seeds", "Maybe a cute flower will grow?", new[] { TileType.Dirt, TileType.WetDirt }, BasicPlantSeedImg, BasicPlantGo, typeof(PlantPower), BasicPlantInfo, ItemID.BasicPlant) }
        };
    }

    public Dictionary<ItemID, Item> AllItems;
}
