using System;
using System.Linq;

public class Inventory
{
    public Inventory()
    {
        const int nbSlots = 15;
        _slots = new Slot[nbSlots];
        for (int i = 0; i < nbSlots; i++)
            _slots[i] = new Slot(null, 0);
    }

    /// <summary>
    /// We don't do that in the ctor cause in case of the remote connection the Player is init before the ItemsList is available
    /// Must only be called by local player
    /// </summary>
    public void InitInventoryContent()
    {
        AddItemInternal(ItemsList.Items.AllItems[ItemID.GunMechanic], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.Generator], 1); // The player begin the game with a generator
        AddItemInternal(ItemsList.Items.AllItems[ItemID.Spade], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.Water], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.BasicPlant], 5);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.Scythe], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.GunEnergy], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.Shotgun], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.Sniper], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.GunBounce], 1);
        AddItemInternal(ItemsList.Items.AllItems[ItemID.DemonicCat], 2);

        UIManager.uiManager.InitInventory(this);
    }

    /// THE FOLLOWING FUNCTIONS MUST ONLY BE CALLED ON THE LOCAL PLAYER

    public void RemoveItem(ItemID id)
    {
        var elem = _slots.Where(x => x.item?.GetId() == id).First();
        if (elem != null && elem.amount > 1)
            elem.amount--;
        else
        {
            int index = Array.IndexOf(_slots, elem);
            _slots[index].item = null;
            _slots[index].amount = 0;
        }
        UIManager.uiManager.UpdateInventory();
        PlayerController.LOCAL.UpdateSelectionColor();
    }

    public void AddItem(ItemID id)
    {
        var elem = _slots.Where(x => x.item?.GetId() == id).FirstOrDefault();
        if (elem != null)
            elem.amount++;
        else
            AddItemInternal(ItemsList.Items.AllItems[id], 1);
        UIManager.uiManager.UpdateInventory();
        PlayerController.LOCAL.UpdateSelectionColor();
    }
    private bool AddItemInternal(Item item, int amount)
    {
        foreach (Slot s in _slots)
        {
            if (s.item == null)
            {
                s.item = item;
                s.amount = amount;
                return true;
            }
        }
        return false;
    }
    public void Swap(ItemID id1, int item2)
    {
        int item1 = -1;
        int i = 0;
        foreach (var s in _slots)
        {
            if (s.item != null && s.item.GetId() == id1)
            {
                item1 = i;
                break;
            }
            i++;
        }
        var tmp = _slots[item1];
        _slots[item1] = _slots[item2];
        _slots[item2] = tmp;
        UIManager.uiManager.UpdateInventory();
        PlayerController.LOCAL.UpdateSelectionColor();
    }

    public Slot[] GetInventory()
        => _slots;

    // TODO: Need to check max size
    private Slot[] _slots; // Items and how many you have

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
