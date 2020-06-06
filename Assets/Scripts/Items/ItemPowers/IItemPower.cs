using UnityEngine;

public interface IItemPower
{
    void OnItemPlaced(PlayerController player, Item item, Vector2Int pos); // TODO: Put that in static (need to wait C#8 support, Unity version 2020.2.0a12)
}
