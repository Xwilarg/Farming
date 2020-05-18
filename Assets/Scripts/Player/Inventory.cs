using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Inventory
{
    public Inventory()
    {
        _slots = new List<Slot>();
    }

    /// <summary>
    /// We don't do that in the ctor cause in case of the remote connection the Player is init before the ItemsList is available
    /// Must only be called by local player
    /// </summary>
    public void InitInventoryContent()
    {
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.Generator], 1)); // The player begin the game with a generator
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.Scythe], 1));
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.BasicPlant], 5));

        _uiActionBar = UIManager.uiManager.GetActionBar();
        _uiActionBar?.InitInventory(this);
    }

    public void RemoveItem(ItemID id)
    {
        var elem = _slots.Where(x => x.item.GetId() == id).First();
        if (elem.amount > 1)
            elem.amount--;
        else
            _slots.Remove(elem);
        _uiActionBar?.UpdateSlots();
    }

    public void AddItem(ItemID id)
    {
        var elem = _slots.Where(x => x.item.GetId() == id).FirstOrDefault();
        if (elem.item != null)
            elem.amount++;
        else
            _slots.Add(new Slot(ItemsList.Items.AllItems[id], 1));
        _uiActionBar?.UpdateSlots();
    }

    public ReadOnlyCollection<Slot> GetInventory()
        => _slots.AsReadOnly();

    private List<Slot> _slots; // Items and how many you have
    private ActionBar _uiActionBar; // A reference to the UI action bar if it's the local player so we can update it

    public class Slot
    {
        public Slot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public Item item;
        public int amount;
    }
}
