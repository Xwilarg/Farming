using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBarSlot : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Image _background;

    [SerializeField]
    private Text _itemCount;

    private Item _item = null;

    public void Select()
    {
        _background.color = Color.yellow;
    }

    public void Unselect()
    {
        _background.color = Color.white;
    }

    public void SetItem(Item item, int amount)
    {
        _image.sprite = item?.GetImage();
        _item = item;
        if (amount == 0)
            _itemCount.text = "";
        else
            _itemCount.text = amount.ToString();
    }

    public Item GetItem()
        => _item;

    public void OnDrag(PointerEventData eventData)
    {
        if (_item == null)
            return;
        _image.transform.position = eventData.position;
        transform.SetAsLastSibling(); // TODO: Probably can only do this on pointer down
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_item == null)
            return;
        UIManager.uiManager.TradeObjectPosition(_item, eventData.position); // TODO: Option to throw the object away
        _image.transform.position = transform.position;
    }
}
