using UnityEngine;

public class ItemsList : MonoBehaviour
{
    public static ItemsList Items { private set; get; }

    private void Start()
    {
        Items = this;
        Generator = new Item("Generator", "Generate a livable space around you", new[] { TileType.Grass, TileType.Sand }, GeneratorImg);
    }

    [SerializeField]
    private Sprite GeneratorImg;

    public Item Generator;
}
