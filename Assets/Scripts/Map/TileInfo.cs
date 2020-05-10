using UnityEngine;

public class TileInfo
{
    public TileInfo(GameObject gameObject)
    {
        _gameObject = gameObject;
        _type = TileType.Sand;
    }

    private GameObject _gameObject;
    private TileType _type;
}
