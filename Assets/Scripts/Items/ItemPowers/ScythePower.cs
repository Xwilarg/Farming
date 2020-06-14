using UnityEngine;

class ScythePower : AItemPower
{
    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        var plant = Generation.GENERATION.GetTile(pos).GetItem();
        var plantId = plant.GetId();
        player.GetPlayer().Inventory.AddItem(plantId);
        if (plant.IsPlantLvlMax())
        {
            player.GetPlayer().Inventory.AddItem(plantId);
            player.GetPlayer().Inventory.AddItem(plantId);
        }
        Generation.GENERATION.DestroyObject(pos);
    }

    public override bool CanBePlaced(TileInfo tile)
        => tile.IsPlant();
}