using UnityEngine;

public class TileInfo
{
    public TileInfo(GameObject gameObject)
    {
        _gameObject = gameObject;
        _type = TileType.Sand;
        _itemContained = null;
        _renderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void AddItem(Item item, GameObject go)
    {
        _itemContained = item;
        _itemContainedGo = go;
    }

    public bool CanAddItem(Item item)
    {
        if (_itemContained != null && item.HaveGameObject()) // If there is always something on the tile and that the current item will add an object
            return false;
        return item.IsTileCorrect(_type);
    }

    public void SetTileType(TileType value, Material m)
    {
        _type = value;
        _renderer.material = m;
    }

    public TileType GetTileType()
        => _type;

    public void UpdateItem()
        => _itemContained?.Update();

    public GameObject GetGameObject() => _itemContainedGo;

    public bool IsPlant() => _itemContained?.IsPlant() ?? false;

    public Item GetItem() => _itemContained;

    private GameObject _gameObject;
    private TileType _type;
    private Item _itemContained;
    private GameObject _itemContainedGo;
    private MeshRenderer _renderer;
}
