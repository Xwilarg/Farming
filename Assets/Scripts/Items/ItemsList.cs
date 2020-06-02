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

    [Header("Prefab GameObject to be put ingame")]
    [SerializeField]
    private GameObject GeneratorGo;
    [SerializeField]
    private GameObject BasicPlantGo;

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
            { ItemID.Water, new Item(ItemID.Water, "Water", "The healthiest of all the drinks", new[] { TileType.Dirt }, WaterImg, null, new WaterPower()) }
        };
    }

    public Dictionary<ItemID, Item> AllItems;
}
