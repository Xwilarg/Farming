
using UnityEngine;

public class GunPower : AItemPower
{
    public GunPower(WeaponInfo info)
    {
        _info = info;
        _timer = 0f;
    }

    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        if (_timer <= 0f)
        {
            player.Shoot(_info);
            _timer = _info.intervalBetweenShoot;
        }
    }

    public override void InventoryUpdate()
    {
        _timer -= Time.deltaTime;
    }

    public Sprite GetCrosshair()
        => _info.crosshair;
    public float GetZoomMultiplicator()
        => _info.zoomMultiplicator;

    private WeaponInfo _info;

    private float _timer;
}