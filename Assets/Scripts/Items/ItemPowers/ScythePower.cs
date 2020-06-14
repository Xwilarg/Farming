using UnityEngine;

class ScythePower : AItemPower
{
    public override void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    {
        var seed = Generation.GENERATION.GetTile(pos).GetItem();
        var plantId = seed.GetPlantID();
        var seedId = seed.GetId();
        player.GetPlayer().Inventory.AddItem(seedId);
        if (seed.IsPlantLvlMax())
        {
            player.GetPlayer().Inventory.AddItem(seedId);
            player.GetPlayer().Inventory.AddItem(seedId);
            player.GetPlayer().Inventory.AddItem(plantId);
        }
        Generation.GENERATION.DestroyObject(pos);
    }

    public override bool CanBePlaced(TileInfo tile)
        => tile.IsPlant();
}