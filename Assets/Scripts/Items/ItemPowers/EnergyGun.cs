using UnityEngine;

public class EnergyGun : IItemPower
{
    public void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        player.Shoot(ItemsInit.S.energyGun);
    }
}
