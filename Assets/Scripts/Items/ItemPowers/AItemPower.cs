using UnityEngine;

public abstract class AItemPower
{
    /// <summary>
    /// Called when the item is placed on the world
    /// </summary>
    public virtual void OnItemPlaced(PlayerController player, Item item, Vector2Int pos)
    { }

    /// <summary>
    /// Called on every update tick on objects that are placed on the world
    /// </summary>
    public virtual void Update()
    { }

    /// <summary>
    /// Called on every update tick on objects that are on the player's inventory
    /// </summary>
    public virtual void InventoryUpdate()
    { }

    /// <summary>
    /// Do additional check to see if the item can be used on the tile
    /// </summary>
    public virtual bool CanBePlaced(TileInfo tile)
        => true;

    /// <summary>
    /// Does display a popup saying "Press E"
    /// </summary>
    /// <returns></returns>
    public virtual bool CanBeInteractedWith()
        => false;

    public virtual void Interact()
    { }
}
