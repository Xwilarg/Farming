using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Inventory
{
    public Inventory()
    {
        _slots = new List<(Item, int)>();
    }

    /// <summary>
    /// We don't do that in the ctor cause in case of the remote connection the Player is init before the ItemsList is available
    /// Must only be called by local player
    /// </summary>
    public void InitInventoryContent()
    {
        _slots.Add((ItemsList.Items.AllItems[ItemID.Generator], 1)); // The player begin the game with a generator

        _uiActionBar = UIManager.uiManager.GetActionBar();
        _uiActionBar?.InitInventory(this);
    }

    public void RemoveItem(ItemID id)
    {
        var elem = _slots.Where(x => x.Item1.GetId() == id).First();
        if (elem.Item2 > 1)
            elem.Item2--;
        else
            _slots.Remove((elem.Item1, 1));
        _uiActionBar?.UpdateSlots();
    }

    public ReadOnlyCollection<(Item, int)> GetInventory()
        => _slots.AsReadOnly();

    private List<(Item, int)> _slots; // Items and how many you have
    private ActionBar _uiActionBar; // A reference to the UI action bar if it's the local player so we can update it
}
