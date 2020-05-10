using UnityEngine;

public class Item
{
    public Item(string name, string description, TileType[] allowedTiles, Sprite img)
    {
        _name = name;
        _description = description;
        _allowedTiles = allowedTiles;
        _img = img;
    }

    public Sprite GetImage() => _img;

    private string _name;
    private string _description;
    private TileType[] _allowedTiles; // Tiles where the object can be placed
    private Sprite _img;
}
