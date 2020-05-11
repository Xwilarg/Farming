using System.Linq;
using UnityEngine;

public class Item
{
    public Item(ItemID id, string name, string description, TileType[] allowedTiles, Sprite img, GameObject go)
    {
        _id = id;
        _name = name;
        _description = description;
        _allowedTiles = allowedTiles;
        _img = img;
    }

    public bool IsTileCorrect(TileType tile)
        => _allowedTiles.Contains(tile);

    public Sprite GetImage() => _img;
    public GameObject GetGameObject() => _go;

    ItemID _id;
    private string _name;
    private string _description;
    private TileType[] _allowedTiles; // Tiles where the object can be placed
    private Sprite _img;
    private GameObject _go;
}
