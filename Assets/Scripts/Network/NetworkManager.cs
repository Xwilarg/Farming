using System.Net.Sockets;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (_client != null)
            _client.Close();
    }

    public void Host()
    {
    }

    public void Connect(string ip, int port)
    {
        _client.Connect(ip, port);
    }

    private TcpClient _client = null;
}
