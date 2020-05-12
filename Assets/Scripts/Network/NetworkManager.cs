using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager NETWORK_MANAGER { private set; get; } // Static reference to this object

    private void Awake()
    {
        NETWORK_MANAGER = this;
    }

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

    /// <summary>
    /// First thing called when the client is connected or the server start
    /// </summary>
    public void SpawnPlayer(Player p, bool isMe, Vector2 pos, Vector2Int vel)
    {
        _toCall.Add(() => { _gm.InstantiatePlayer(p, this, isMe, pos, vel); });
    }

    /// <summary>
    /// Instantiate an object over the network
    /// </summary>
    /// <returns>Returns true if the object can be instanted right now</returns>
    public bool RequestItemSpawn(ItemID id, Vector2Int position)
    {
        // If we are a distant player we must at first send a request to the server
        using (MemoryStream ms = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(ms))
            {
                writer.Write((byte)id);
                writer.Write(position);
                if (_isHostLocalPlayer)
                    _server.SendToEveryone(NetworkRequest.ObjectInstantiate, ms.ToArray(), -1);
                else
                    _client.SendRequest(NetworkRequest.ObjectInstantiate, ms.ToArray());
            }
        }
        return _isHostLocalPlayer; // The host can start instantiating the object right away
    }

    private NetworkClient _client = null;
    private NetworkServer _server = null;

    private bool _isHostLocalPlayer;
    private List<Action> _toCall;

    // Everything related to when the client and server must first connect
    #region NetworkConnection
    public void Host(int port)
    {
        _isHostLocalPlayer = true;
        // We launch the server and spawn our player right when the main scene is done loading
        _server = new NetworkServer(this, port);
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Main")
            {
                _me = new Player(null, 0, true);
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
    #endregion NetworkConnection

    private Player _me;
    private List<Player> _players;
    public void AddPlayer(Player p)
        => _players.Add(p);
    public List<Player> GetPlayers()
        => _players;
    public Player GetPlayer(int id)
        => _players.Where(x => x.Id == id).First();
    public void SetMe(Player me)
        => _me = me;
    public Player GetMe()
        => _me;

    private GameManager _gm;
}
