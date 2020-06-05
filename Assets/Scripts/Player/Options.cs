using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    private Transform _optionPanel;
    private Console _console;
    
    private void Start()
    {
        _optionPanel = transform.GetChild(0);
        _console = null;
    }

    public void RespawnConsole()
    {
        if (_console == null)
            _console = GameObject.FindGameObjectWithTag("DebugManager").GetComponent<Console>();
        _console.Open();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (_optionPanel.gameObject.activeInHierarchy)
            {
                _optionPanel.gameObject.SetActive(false);
                _console?.Close();
                if (SceneManager.GetActiveScene().name == "Main")
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            else
            {
                _optionPanel.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        };
    }
}
