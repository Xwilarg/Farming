using System.Collections.Generic;

public class Inventory
{
    public Inventory()
    {
        _slots = new List<(Item, int)>();
    }

    private List<(Item, int)> _slots; // Items and how many you have
}
