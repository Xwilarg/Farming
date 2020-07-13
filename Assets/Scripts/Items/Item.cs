using System;
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
    public Item(ItemID id, string name, string description, TileType[] allowedTiles, Sprite img, GameObject go, int price, Type powerType, params object[] powerArgs)
    {
        _id = id;
        _name = name;
        _description = description;
        _allowedTiles = allowedTiles;
        _img = img;
        _go = go;
        _price = price;

        _power = (AItemPower)Activator.CreateInstance(powerType, powerArgs);
        _powerType = powerType;
        _powerArgs = powerArgs;
    }

    public Item(Item item)
    {
        _id = item._id;
        _name = item._name;
        _description = item._description;
        _allowedTiles = item._allowedTiles;
        _img = item._img;
        _go = item._go;

        _power = (AItemPower)Activator.CreateInstance(item._powerType, item._powerArgs);
        _powerType = item._powerType;
        _powerArgs = item._powerArgs;
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

    public void InventoryUpdate()
    {
        _power?.InventoryUpdate();
    }

    public bool CanBeInteractedWith()
    {
        return _power?.CanBeInteractedWith() ?? false;
    }

    public void Interact()
    {
        _power.Interact();
    }

    public Sprite GetImage() => _img;
    public GameObject GetGameObject() => _go;
    public bool HaveGameObject() => _go != null;

    public ItemID GetId() => _id;
    public string GetName() => _name;
    public string GetDescription() => _description;
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
    public bool HaveAutoFire()
    {
        if (_power == null)
            return false;
        var gunPower = _power as GunPower;
        if (gunPower == null)
            return false;
        return gunPower.IsAuto();
    }

    /// FOR PLANTS
    public bool IsPlant()
        => _power is PlantPower;

    public bool IsPlantLvlMax()
    {
        if (!IsPlant())
            return false;
        return ((PlantPower)_power).IsLevelMax();
    }

    /// <summary>
    /// Get the ID of the plant that a seed is growing
    /// </summary>
    public ItemID GetPlantID()
    {
        return ((PlantPower)_power).GetPlantID();
    }

    public bool CanBePlaced(TileInfo tile)
    {
        return _power?.CanBePlaced(tile) ?? true;
    }

    public int GetPrice()
        => _price;

    private ItemID _id;
    private string _name;
    private string _description;
    private TileType[] _allowedTiles; // Tiles where the object can be placed
    private Sprite _img;
    private GameObject _go;
    private int _price;

    private AItemPower _power;
    private Type _powerType;
    private object[] _powerArgs;
}
