using UnityEngine;

public abstract class AItemPower
{
    /// <summary>
    /// Called when the item is placed on the world
    /// </summary>
    public virtual void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    { }

    /// <summary>
    /// Called on every update tick
    /// </summary>
    public virtual void Update()
    { }

    /// <summary>
    /// Do additional check to see if the item can be used on the tile
    /// </summary>
    public virtual bool CanBePlaced(TileInfo tile)
        => true;
}
