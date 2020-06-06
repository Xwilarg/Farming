using UnityEngine;

public class MechanicalGun : IItemPower
{
    public void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        player.Shoot(ItemsInit.S.mechanicalGun);
    }
}