using UnityEngine;

public class TileInfo
{
    public TileInfo(GameObject gameObject)
    {
        _gameObject = gameObject;
        _type = TileType.Sand;
        _itemContained = null;
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

    private GameObject _gameObject;
    private TileType _type;
    private Item _itemContained;
    private GameObject _itemContainedGo;
}
