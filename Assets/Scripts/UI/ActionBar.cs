using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField]
    private ActionBarKey[] _actions;

    private ActionBarSlot _selected = null;

    private void Start()
    {
        SelectSlot(_actions[0].slot);
    }

    public ActionBarKey[] GetActions() => _actions;

    public void UpdateSlots(Inventory.Slot[] items)
    {
        int maxItems = _actions.Length < items.Length ? _actions.Length : items.Length;
        int i;
        for (i = 0; i < maxItems; i++)
        {
            _actions[i].slot.SetItem(items[i].item, items[i].amount);
        }
        for (; i < _actions.Length; i++)
        {
            _actions[i].slot.SetItem(null, 0);
        }
    }

    public void SelectSlot(ActionBarSlot slot)
    {
        if (_selected != null)
            _selected.Unselect();
        _selected = slot;
        _selected.Select();
    }

    public Item GetCurrentlySelectedItem()
        => _selected.GetItem();

    public IEnumerable<ActionBarSlot> GetActionBarSlots()
        => _actions.Select(x => x.slot);
}
