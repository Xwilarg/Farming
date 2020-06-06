using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.GunEnergy], 1));
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.GunMechanic], 1));
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.Generator], 1)); // The player begin the game with a generator
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.Spade], 1));
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.Water], 1));
        _slots.Add(new Slot(ItemsList.Items.AllItems[ItemID.BasicPlant], 5));

        UIManager.uiManager.InitInventory(this);
    }

    /// THE FOLLOWING FUNCTIONS MUST ONLY BE CALLED ON THE LOCAL PLAYER

    public void RemoveItem(ItemID id)
    {
        var elem = _slots.Where(x => x.item.GetId() == id).First();
        if (elem.amount > 1)
            elem.amount--;
        else
            _slots.Remove(elem);
        UIManager.uiManager.UpdateInventory();
        PlayerController.LOCAL.UpdateSelectionColor();
    }

    public void AddItem(ItemID id)
    {
        var elem = _slots.Where(x => x.item.GetId() == id).FirstOrDefault();
        if (elem.item != null)
            elem.amount++;
        else
            _slots.Add(new Slot(ItemsList.Items.AllItems[id], 1));
        UIManager.uiManager.UpdateInventory();
        PlayerController.LOCAL.UpdateSelectionColor();
    }
    public void Swap(ItemID id1, ItemID id2)
    {
        var item1 = new Slot(_slots.Where(x => x.item.GetId() == id1).ElementAt(0));
        var item2 = new Slot(_slots.Where(x => x.item.GetId() == id2).ElementAt(0));
        for (int i = _slots.Count - 1; i >= 0; i--)
        {
            var item = _slots[i];
            if (item1.item.GetId() == item.item.GetId())
                _slots[i] = item2;
            else if (item2.item.GetId() == item.item.GetId())
                _slots[i] = item1;
        }
        UIManager.uiManager.UpdateInventory();
        PlayerController.LOCAL.UpdateSelectionColor();
    }

    public ReadOnlyCollection<Slot> GetInventory()
        => _slots.AsReadOnly();

    // TODO: Need to check max size
    private List<Slot> _slots; // Items and how many you have

    public class Slot
    {
        public Slot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public Slot(Slot slot)
        {
            item = slot.item;
            amount = slot.amount;
        }

        public Item item;
        public int amount;
    }
}
