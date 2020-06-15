using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private Text _title, _description;

    public static Popup S;

    private void Awake()
    {
        S = this;
        gameObject.SetActive(false);
    }

    public void Show(string titleText, string descriptionText)
    {
        _title.text = titleText;
        _description.text = descriptionText;
        gameObject.SetActive(true);
    }
}
