﻿using System.Collections.Generic;
using System.Linq;
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
    private List<ActionBarSlot> _inventorySlots;

    public void SetPlayerInfo(PlayerInfo info)
        => _info = info;

    public ActionBar GetActionBar() => _actionBar;

    private void Start()
    {
        _inventorySlots = _inventoryPanel.GetComponentsInChildren<ActionBarSlot>().ToList();
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
        int i;
        for (i = 0; i < items.Count; i++)
        {
            _inventorySlots[i].SetItem(items[i].item, items[i].amount);
        }
        for (; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].SetItem(null, 0);
        }
    }

    private void Update()
    {
        if (_info == null)
            return;
        if (Input.GetKeyDown(_info.inventoryKey)) // TODO: Need to lock camera movements when triggered
        {
            if (_inventoryPanel.activeInHierarchy)
            {
                _inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                _inventoryPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    
    public bool TradeObjectPosition(Item item, Vector2 pos)
    {
        foreach (var slot in _inventorySlots)
        {
            var tmp = TradeObjectPosition(slot, item, pos);
            if (tmp.HasValue)
                return tmp.Value;
        }
        foreach (var slot in _actionBar.GetActionBarSlots())
        {
            var tmp = TradeObjectPosition(slot, item, pos);
            if (tmp.HasValue)
                return tmp.Value;
        }
        return false;
    }

    private bool? TradeObjectPosition(ActionBarSlot slot, Item item, Vector2 pos)
    {
        var rTransform = (RectTransform)slot.transform;
        var oPos = rTransform.position;
        var s = rTransform.sizeDelta.x / 2f; // It's a square to X is equal to Y
        if (pos.x > oPos.x - s && pos.x < oPos.x + s
            && pos.y > oPos.y - s && pos.y < oPos.y + s)
        {
            var oItem = slot.GetItem();
            if (oItem == null || item.GetId() == oItem.GetId())
                return false;
            _inventory.Swap(item.GetId(), oItem.GetId());
            return true;
        }
        return null;
    }
}
