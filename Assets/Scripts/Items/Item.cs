using System.Linq;
using UnityEngine;

public class Item
{
    /// <summary>
    /// An ingame item
    /// </summary>
    /// <param name="id">ID of the item</param>
    /// <param name="name">Name of the item</param>
    /// <param name="description">Description of the item</param>
    /// <param name="allowedTiles">Tiles where the item can be placed</param>
    /// <param name="img">Sprite to be displayed in the UI</param>
    /// <param name="go">Prefab of the object</param>
    /// <param name="power">Special properties of the item</param>
    public Item(ItemID id, string name, string description, TileType[] allowedTiles, Sprite img, GameObject go, AItemPower power)
    {
        _id = id;
        _name = name;
        _description = description;
        _allowedTiles = allowedTiles;
        _img = img;
        _go = go;
        _power = power;
    }

    public bool IsTileCorrect(TileType tile)
        => _allowedTiles.Contains(tile) || _allowedTiles.Contains(TileType.All);

    /// <summary>
    /// Must be called when the item is put on the floor in the game
    /// </summary>
    public void Place(PlayerController player, Vector2Int pos)
    {
        _power?.OnItemPlaced(player, this, pos);
    }

    public void Update()
    {
        _power?.Update();
    }

    public Sprite GetImage() => _img;
    public GameObject GetGameObject() => _go;
    public bool HaveGameObject() => _go != null;

    public ItemID GetId() => _id;
    public Sprite GetCrosshair()
    {
        if (_power == null)
            return null;
        return (_power as GunPower)?.GetCrosshair();
    }
    public float GetZoomMultiplicator()
    {
        if (_power == null)
            return 1;
        var gunPower = _power as GunPower;
        if (gunPower == null)
            return 1;
        return gunPower.GetZoomMultiplicator();
    }

    private ItemID _id;
    private string _name; public string GetName() => _name;
    private string _description; public string GetDescription() => _description;
    private TileType[] _allowedTiles; // Tiles where the object can be placed
    private Sprite _img;
    private GameObject _go;
    private AItemPower _power;
}
