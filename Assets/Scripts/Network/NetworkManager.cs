using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
    public void SpawnPlayer(bool isMe, Vector3 pos, Vector3 vel)
    {
        _gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); // We need to wait to be on the Main scene to do that
        _gm.InstantiatePlayer(isMe, pos, vel);
    }

    private NetworkClient _client = null;
    private NetworkServer _server = null;

    private GameManager _gm;
}
