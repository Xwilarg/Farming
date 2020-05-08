using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DrawGrid))]
public class Console : MonoBehaviour
{
    [SerializeField]
    private GameObject _consoleGo;

    [SerializeField]
    private InputField _input, _output;

    private DrawGrid _grid;

    private Player _player; // Reference to the Player
    private bool _canSpawnPlayer = false;

    public void EnablePlayerSpawn(Player p)
    {
        _player = p;
        _canSpawnPlayer = true;
    }

    private Dictionary<string, ConsoleCommand> _commands;

    private void Start()
    {
        _commands = new Dictionary<string, ConsoleCommand>()
        {
            { "respawn", new ConsoleCommand{ argumentCount = 0, callback = Respawn } },
            { "grid", new ConsoleCommand{ argumentCount = 0, callback = Grid } },
            { "hide", new ConsoleCommand{ argumentCount = 0, callback = Hide } }
        };

        _grid = GetComponent<DrawGrid>();
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
        string[] text = _input.text.Trim().ToLower().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        CleanInput();
        if (text.Length == 0)
            return;
        _output.text = "> " + string.Join(" ", text) + "\n\n";
        if (_commands.ContainsKey(text[0]))
            _commands[text[0]].callback(text.Skip(1).ToArray());
        else
            _output.text += "Command not found";
        _input.Select();
    }

    private void Respawn(string[] _)
    {
        if (!_canSpawnPlayer)
        {
            _output.text += "Can't spawn player at this point of the game";
            return;
        }
        if (_player.Pc != null)
        {
            Camera.main.transform.parent = null; // We make sure to not delete the main camera
            Destroy(_player.Pc.gameObject);
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().InstantiatePlayer(_player, null, true, Vector2.zero, Vector2Int.zero);
        _output.text += "Player respawed at (0;0)";
    }

    private void Grid(string[] _)
    {
        _grid.ToggleGrid();
        _output.text += "Grid display status changed (using gizmos)";
    }

    private void Hide(string[] _)
    {
        var mesh = GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>();
        mesh.enabled = !mesh.enabled;
        _output.text += "Player rendering is now set to " + mesh.enabled;
    }
}
