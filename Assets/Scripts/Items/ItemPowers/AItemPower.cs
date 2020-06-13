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
}
