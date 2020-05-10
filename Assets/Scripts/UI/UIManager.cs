using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManager { private set; get; }

    private void Start()
    {
        uiManager = this;
    }

    [SerializeField]
    private ActionBar _actionBar;

    public ActionBar GetActionBar() => _actionBar;
}
