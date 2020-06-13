using UnityEngine;

class SpadePower : AItemPower
{
    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        Generation.GENERATION.ChangeFloorType(pos, TileType.Dirt);
    }
}