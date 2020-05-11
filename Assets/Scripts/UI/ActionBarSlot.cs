using UnityEngine;
using UnityEngine.UI;

public class ActionBarSlot : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Image _background;

    private Item _item = null;

    public void Select()
    {
        _background.color = Color.yellow;
    }

    public void Unselect()
    {
        _background.color = Color.white;
    }

    public void SetItem(Item item)
    {
        _image.sprite = item?.GetImage();
        _item = item;
    }

    public Item GetItem()
        => _item;
}
