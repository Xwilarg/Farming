using UnityEngine;
using UnityEngine.UI;

public class ActionBarSlot : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Image _background;

    public void Select()
    {
        _background.color = Color.yellow;
    }

    public void Unselect()
    {
        _background.color = Color.white;
    }
}
