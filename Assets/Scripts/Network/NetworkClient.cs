using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class NetworkClient
{
    public NetworkClient(NetworkManager manager, string ip, int port)
    {
        _manager = manager;

        _tcp = new TCPWrapper(new TcpClient(ip, port), GetRequest, 0);
    }

    private void GetRequest(TCPWrapper _, NetworkRequest type, byte[] payload)
    {
        MemoryStream stream = new MemoryStream(payload);
        BinaryReader reader = new BinaryReader(stream);
        switch (type)
        {
            case NetworkRequest.CriticalError:
                throw new System.Exception(reader.ReadString());

            case NetworkRequest.AuthentificationSuccess:
                _tcp.SetId(reader.ReadByte());
                _manager.SpawnPlayer(true, Vector3.up, Vector3.zero);
                break;
        }
    }

    private NetworkManager _manager;

    private TCPWrapper _tcp;
}
