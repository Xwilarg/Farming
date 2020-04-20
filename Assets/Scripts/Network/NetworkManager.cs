using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Host(int port)
    {
        _server = new NetworkServer(port);
    }

    public void Connect(string ip, int port)
    {
        _client = new NetworkClient(ip, port);
    }

    NetworkClient _client = null;
    NetworkServer _server = null;
}
