using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DrawGrid))]
public class ToggleConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject _consoleGo;

    [SerializeField]
    private InputField _input, _output;

    private DrawGrid _grid;

    private Player _player;

    private void Start()
    {
        _grid = GetComponent<DrawGrid>();
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
        if (text == "respawn")
        {
            if (_player.Pc != null)
            {
                Camera.main.transform.parent = null; // We make sure to not delete the main camera
                Destroy(_player.Pc.gameObject);
            }
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().InstantiatePlayer(_player, null, true, Vector2.zero, Vector2Int.zero);
            _output.text += "Player respawed at (0;0)";
        }
        else if (text == "grid")
        {
            _grid.ToggleGrid();
            _output.text += "Grid display status changed (using gizmos)";
        }
        else if (text == "hide")
        {
            var mesh = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>();
            mesh.enabled = !mesh.enabled;
            _output.text += "Player rendering is now set to " + mesh.enabled;
        }
        else
        {
            _output.text += "Command not found";
        }
        _input.Select();
    }
}
