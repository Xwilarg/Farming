using UnityEngine;
using UnityEngine.UI;

public class ToggleConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject _consoleGo;

    [SerializeField]
    private InputField _input, _output;

    private Player _player;

    private void Start()
    {
        _player = new Player(null, 255);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (_consoleGo.activeInHierarchy)
                _consoleGo.SetActive(false);
            else
            {
                _consoleGo.SetActive(true);
                _input.Select();
            }
        }
        if (Input.GetKeyUp(KeyCode.Return) && _consoleGo.activeInHierarchy)
            Send();
    }

    private void CleanInput()
    {
        _input.text = "";
    }

    public void Send()
    {
        string text = _input.text;
        CleanInput();
        if (string.IsNullOrWhiteSpace(text))
            return;
        _output.text = "> " + text + "\n\n";
        if (text.StartsWith("respawn"))
        {
            if (_player.Pc != null)
                Destroy(_player.Pc.gameObject);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().InstantiatePlayer(_player, null, true, Vector2.zero, Vector2Int.zero);
            _output.text += "Player respawed at (0;0)";
        }
        else
        {
            _output.text += "Command not found";
        }
        _input.Select();
    }
}
