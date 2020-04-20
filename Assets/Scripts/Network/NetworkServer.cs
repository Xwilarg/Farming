using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkServer
{
    public NetworkServer(NetworkManager manager, int port)
    {
        _idCount = 0;
        _manager = manager;
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();
        _clients = new List<TCPWrapper>();
        CreateNewClientThread();

        manager.SpawnPlayer(true, Vector3.up, Vector3.zero);
    }

    public void CreateNewClientThread()
    {
        Task.Run(() =>
        {
            _clients.Add(new TCPWrapper(_listener.AcceptTcpClient(), GetRequest, _idCount++));
            CreateNewClientThread();
        });
    }

    private void GetRequest(TCPWrapper tcp, NetworkRequest type, byte[] payload)
    {
        MemoryStream s = new MemoryStream(payload);
        BinaryReader reader = new BinaryReader(s);
        switch (type)
        {
            case NetworkRequest.Authentification:
                if (reader.ReadString() == NetworkConstants._authKey)
                {
                    SendRequest(tcp, NetworkRequest.AuthentificationSuccess);
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write((byte)NetworkRequest.CriticalError);
                    writer.Write("Wrong authentification key");
                }
                break;
        }
    }

    private void SendRequest(TCPWrapper c, NetworkRequest type)
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        switch (type)
        {
            case NetworkRequest.AuthentificationSuccess:
                writer.Write(c.GetId());
                c.SendRequest(NetworkRequest.AuthentificationSuccess, stream.ToArray());
                break;
        }
    }

    public void SendToEveryone(NetworkRequest type, byte[] payload, int except)
    {
        foreach (TCPWrapper c in _clients)
        {
            if (c.GetId() != except)
                c.SendRequest(type, payload);
        }
    }

    private TcpListener _listener;
    private List<TCPWrapper> _clients;

    private NetworkManager _manager;

    private byte _idCount;
}
