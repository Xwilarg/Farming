using UnityEngine;
using UnityEngine.UI;

public class ActionBarSlot : MonoBehaviour
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
}
