using UnityEngine;

public class MechanicalGunPower : IItemPower
{
    public void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        player.Shoot(ItemsInit.S.mechanicalGun);
    }
}