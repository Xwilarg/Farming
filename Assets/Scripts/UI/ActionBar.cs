using System.Collections.ObjectModel;
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

    private void Update()
    {
        foreach (var action in _actions) // We check if any key of the action bar is pressed
            if (Input.GetKeyDown(action.key))
            {
                SelectSlot(action.slot);
                PlayerController.LOCAL.UpdateSelectionColor();
            }
    }

    public void UpdateSlots(ReadOnlyCollection<Inventory.Slot> items)
    {
        int maxItems = _actions.Length < items.Count ? _actions.Length : items.Count;
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

    private void SelectSlot(ActionBarSlot slot)
    {
        if (_selected != null)
            _selected.Unselect();
        _selected = slot;
        _selected.Select();
    }

    public Item GetCurrentlySelectedItem()
        => _selected.GetItem();
}
