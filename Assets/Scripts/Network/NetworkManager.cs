using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
        // We launch the server and spawn our player right when the main scene is done loading
        _server = new NetworkServer(this, port);
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Main")
                _server.SpawnPlayer();
        };
    }

    public void Connect(string ip, int port)
    {
        _client = new NetworkClient(this, ip, port);
    }

    // First thing called when the client is connected or the server start
    public void SpawnPlayer(bool isMe, Vector2 pos, Vector2 vel)
    {
        _toCall.Add(() => { _gm.InstantiatePlayer(isMe, new Vector3(pos.x, 1f, pos.y), new Vector3(vel.x, 0f, vel.y)); });
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

    private NetworkClient _client = null;
    private NetworkServer _server = null;

    private GameManager _gm;

    private List<Action> _toCall;
}
