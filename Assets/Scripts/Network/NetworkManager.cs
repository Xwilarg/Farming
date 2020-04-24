using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _players = new List<Player>();
        _toCall = new List<Action>();
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Main")
            {
                _gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
        };
    }

    public void Host(int port)
    {
        _isHostLocalPlayer = true;
        // We launch the server and spawn our player right when the main scene is done loading
        _server = new NetworkServer(this, port);
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Main")
            {
                _me = new Player(null, 0);
                _players.Add(_me);
                _server.SpawnPlayer(_me);
            }
        };
    }

    public void Connect(string ip, int port)
    {
        _isHostLocalPlayer = false;
        _client = new NetworkClient(this, ip, port);
    }

    // First thing called when the client is connected or the server start
    public void SpawnPlayer(Player p, bool isMe, Vector2 pos, Vector2Int vel)
    {
        _toCall.Add(() => { _gm.InstantiatePlayer(p, this, isMe, pos, vel); });
    }

    private void Update()
    {
        // We can only do some stuffs from the main thread so we store them in "_toCall"
        if (_toCall.Count > 0 && _gm != null)
        {
            foreach (var fct in _toCall)
                fct();
            _toCall = new List<Action>();
        }
    }

    /// <summary>
    /// If server: send to everyone
    /// If client: send to server
    /// </summary>
    public void SendRequest(NetworkRequest request, byte[] data)
    {
        if (_isHostLocalPlayer)
            _server.SendToEveryone(request, data, -1);
        else
            _client.SendRequest(request, data);
    }

    private List<Player> _players;
    private Player _me;
    public void AddPlayer(Player p)
        => _players.Add(p);
    public List<Player> GetPlayers()
        => _players;
    public void SetMe(Player me)
        => _me = me;
    public Player GetMe()
        => _me;

    private NetworkClient _client = null;
    private NetworkServer _server = null;

    private bool _isHostLocalPlayer;

    private GameManager _gm;

    private List<Action> _toCall;
}
