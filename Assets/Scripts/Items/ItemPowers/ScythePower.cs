class ScythePower : AItemPower
{
    public override bool CanBePlaced(TileInfo tile)
        => tile.IsPlant();
}