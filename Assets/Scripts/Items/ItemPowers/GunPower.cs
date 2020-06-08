
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

    private WeaponInfo _info;
}