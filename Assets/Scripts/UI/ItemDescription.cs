using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [SerializeField]
    private Text title, description;

    public void SetDescription(Item item)
    {
        if (item != null)
        {
            title.text = item.GetName();
            description.text = item.GetDescription();
        }
        else
        {
            title.text = "";
            description.text = "";
        }
    }
}
