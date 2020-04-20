using UnityEngine;

public class ToggleConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject _consoleGo;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            _consoleGo.SetActive(!_consoleGo.activeInHierarchy);
    }
}
