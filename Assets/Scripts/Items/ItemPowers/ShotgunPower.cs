using UnityEngine;

public class ShotgunPower : IItemPower
{
    public void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        player.Shoot(ItemsInit.S.shotgun);
    }
}