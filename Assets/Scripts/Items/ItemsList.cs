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
    [SerializeField]
    private Sprite ShopImg;

    [Header("Prefab GameObject to be put ingame")]
    [SerializeField]
    private GameObject GeneratorGo;
    [SerializeField]
    private GameObject BasicPlantGo;
    [SerializeField]
    private GameObject EnemyGo;
    [SerializeField]
    private GameObject ChestGo;
    [SerializeField]
    private GameObject ShopGo;

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

    [Header("Object prices")]
    [SerializeField]
    private int GeneratorPrice;
    [SerializeField]
    private int BasicPlantPrice;
    [SerializeField]
    private int ScythePrice;
    [SerializeField]
    private int SpadePrice;
    [SerializeField]
    private int WaterPrice;
    [SerializeField]
    private int GunEnergyPrice;
    [SerializeField]
    private int GunMechanicPrice;
    [SerializeField]
    private int ShotgunPrice;
    [SerializeField]
    private int DemonicCatPrice;
    [SerializeField]
    private int SniperPrice;
    [SerializeField]
    private int GunBouncePrice;
    [SerializeField]
    private int ChestPrice;
    [SerializeField]
    private int BasicPlantSeedPrice;
    [SerializeField]
    private int ShopPrice;

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
            { ItemID.Generator, new Item(ItemID.Generator, "Generator", "Generate a livable space around you", new[] { TileType.Grass, TileType.Sand }, GeneratorImg, GeneratorGo, GeneratorPrice, typeof(GeneratorPower)) },
            { ItemID.BasicPlant, new Item(ItemID.BasicPlant, "Basic Plant", "Just a basic plant, you can probably sell it", new TileType[0], BasicPlantImg, null, BasicPlantPrice, typeof(EmptyPower)) },
            { ItemID.Scythe, new Item(ItemID.Scythe, "Scythe", "To cut down plants and annoying salesman", new [] { TileType.Dirt, TileType.WetDirt }, ScytheImg, null, ScythePrice, typeof(ScythePower)) },
            { ItemID.Spade, new Item(ItemID.Spade, "Spade", "Plow the earth to make a comfy place for your plants", new[] { TileType.Grass }, SpadeImg, null, SpadePrice, typeof(SpadePower)) },
            { ItemID.Water, new Item(ItemID.Water, "Water", "The healthiest of all the drinks", new[] { TileType.Dirt, TileType.WetDirt }, WaterImg, null, WaterPrice, typeof(WaterPower)) },
            { ItemID.GunEnergy, new Item(ItemID.GunEnergy, "Gun (Energy)", "A basic handgun, especially strong against mechanic shields", new[] { TileType.Special }, GunEnergyImg, null, GunEnergyPrice, typeof(GunPower), GunEnergyInfo) },
            { ItemID.GunMechanic, new Item(ItemID.GunMechanic, "Gun (Mechanic)", "A basic handgun, especially strong against energy shields", new[] { TileType.Special }, GunMechanicImg, null, GunMechanicPrice, typeof(GunPower), GunMechanicalInfo) },
            { ItemID.Shotgun, new Item(ItemID.Shotgun, "Shotgun", "An alternative way to open doors", new[] { TileType.Special }, ShotgunImg, null, ShotgunPrice, typeof(GunPower), ShotgunInfo) },
            { ItemID.DemonicCat, new Item(ItemID.DemonicCat, "Demonic Cat", "Devilishly cute", new[] { TileType.Sand }, EnemyImg, EnemyGo, DemonicCatPrice, typeof(EmptyPower)) },
            { ItemID.Sniper, new Item(ItemID.Sniper, "Sniper", "To shoot enemies from the safety of your home", new[] { TileType.Special }, SniperImg, null, SniperPrice, typeof(GunPower), SniperInfo) },
            { ItemID.GunBounce, new Item(ItemID.GunBounce, "Bounce Gun", "It's like a regular gun but more annoying", new[] { TileType.Special }, GunBounceImg, null, GunBouncePrice, typeof(GunPower), GunBounceInfo) },
            { ItemID.Chest, new Item(ItemID.Chest, "Chest", "How can anyone resist this cute chest", new[] { TileType.All }, ChestImg, ChestGo, ChestPrice, typeof(EmptyPower)) },
            { ItemID.BasicPlantSeed, new Item(ItemID.BasicPlantSeed, "Basic Plant Seeds", "Maybe a cute flower will grow?", new[] { TileType.Dirt, TileType.WetDirt }, BasicPlantSeedImg, BasicPlantGo, BasicPlantPrice, typeof(PlantPower), BasicPlantInfo, ItemID.BasicPlant) },
            { ItemID.Shop, new Item(ItemID.Shop, "Shop", "Once placed you'll be able to buy and sell stuffs from it", new[] { TileType.Grass }, ShopImg, ShopGo, ShopPrice, typeof(ShopPower)) }
        };
    }

    public Dictionary<ItemID, Item> AllItems;
}
