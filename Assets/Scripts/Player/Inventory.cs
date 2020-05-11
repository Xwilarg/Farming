using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Inventory
{
    public Inventory(bool isLocalPlayer)
    {
        _slots = new List<(Item, int)>();
        _slots.Add((ItemsList.Items.AllItems[ItemID.Generator], 1)); // The player begin the game with a generator

        if (isLocalPlayer)
        {
            _uiActionBar = UIManager.uiManager.GetActionBar();
            _uiActionBar.InitInventory(this);
        }
    }

    public ReadOnlyCollection<(Item, int)> GetInventory()
        => _slots.AsReadOnly();

    private List<(Item, int)> _slots; // Items and how many you have
    private ActionBar _uiActionBar; // A reference to the UI action bar if it's the local player so we can update it
}
