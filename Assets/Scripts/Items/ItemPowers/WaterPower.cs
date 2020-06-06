using UnityEngine;

class WaterPower : IItemPower
{
    public void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        Generation.GENERATION.ChangeFloorType(pos, TileType.WetDirt);
        ItemsManager.S.AddWetDirt(pos);
    }
}