using UnityEngine;

class WaterPower : AItemPower
{
    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        Generation.GENERATION.ChangeFloorType(pos, TileType.WetDirt);
        ItemsManager.S.AddWetDirt(pos);
    }
}