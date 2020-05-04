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
                SelectSlot(action.slot);
    }

    private void SelectSlot(ActionBarSlot slot)
    {
        if (_selected != null)
            _selected.Unselect();
        _selected = slot;
        _selected.Select();
    }
}
