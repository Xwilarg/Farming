using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { private set; get; }

    [SerializeField]
    private ActionBar _actionBar;

    [SerializeField]
    private GameObject _inventoryPanel;
    private void Awake()
    {
        uiManager = this;
    }

    private PlayerInfo _info = null; // Keep track of keys to press to toggle different UI parts
    private Inventory _inventory;
    private ActionBarSlot[] _inventorySlots;

    public void SetPlayerInfo(PlayerInfo info)
        => _info = info;

    public ActionBar GetActionBar() => _actionBar;

    private void Start()
    {
        _inventorySlots = _inventoryPanel.GetComponentsInChildren<ActionBarSlot>();
    }

    public void InitInventory(Inventory inventory)
    {
        _inventory = inventory;
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        var items = _inventory.GetInventory();
        _actionBar.UpdateSlots(items);
        for (int i = 0; i < items.Count; i++)
        {
            _inventorySlots[i].SetItem(items[i].item, items[i].amount);
        }
    }

    private void Update()
    {
        if (_info == null)
            return;
        if (Input.GetKeyDown(_info.inventoryKey))
            _inventoryPanel.SetActive(!_inventoryPanel.activeInHierarchy);
    }
}
