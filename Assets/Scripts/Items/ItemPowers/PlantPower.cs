
using UnityEngine;

public class PlantPower : AItemPower
{
    public PlantPower(PlantInfo info)
    {
        _info = info;
        _currPhase = 0;
        _timer = info.TimeBetweenStates;
        _tile = null;
    }

    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        _tile = Generation.GENERATION.GetTile(pos);
        _mesh = _tile.GetGameObject().GetComponent<MeshFilter>();
    }

    public override void Update()
    {
        if (_tile != null && _currPhase < _info.States.Length - 1)
        {
            if (!_info.doesNeedWater || _tile.GetTileType() == TileType.WetDirt)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    _currPhase++;
                    _mesh.mesh = _info.States[_currPhase];
                    _timer = _info.TimeBetweenStates;
                }
            }
        }
    }

    public bool IsLevelMax()
        => _currPhase == _info.States.Length - 1;

    private PlantInfo _info;
    private TileInfo _tile;
    private MeshFilter _mesh;
    private int _currPhase;
    private float _timer;
}