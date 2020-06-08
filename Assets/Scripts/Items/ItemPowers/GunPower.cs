
using UnityEngine;

public class GunPower : IItemPower
{
    public GunPower(WeaponInfo info)
    {
        _info = info;
    }

    public void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        player.Shoot(_info);
    }

    public Sprite GetCrosshair()
        => _info.crosshair;
    public float GetZoomMultiplicator()
        => _info.zoomMultiplicator;

    private WeaponInfo _info;
}