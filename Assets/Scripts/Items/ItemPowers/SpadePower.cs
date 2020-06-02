using UnityEngine;

class SpadePower : IItemPower
{
    public void OnItemPlaced(Item item, Vector2Int pos)
    {
        Generation.GENERATION.ChangeFloorType(pos, TileType.Dirt);
    }
}