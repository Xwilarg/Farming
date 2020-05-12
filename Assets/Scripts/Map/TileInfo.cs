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
        if (_itemContained != null)
            return false;
        return item.IsTileCorrect(_type);
    }

    public void SetTileType(TileType value, Material m)
    {
        _type = value;
        _renderer.material = m;
    }

    private GameObject _gameObject;
    private TileType _type;
    private Item _itemContained;
    private GameObject _itemContainedGo;
    private MeshRenderer _renderer;
}
