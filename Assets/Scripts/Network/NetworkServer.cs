using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkServer
{
    public NetworkServer(NetworkManager manager, int port)
    {
        _idCount = 1;
        _manager = manager;
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();
        CreateNewClientThread();
    }

    public void CreateNewClientThread()
    {
        Task.Run(() =>
        {
            _manager.AddPlayer(new Player(new TCPWrapper(_listener.AcceptTcpClient(), GetRequest), _idCount++));
            CreateNewClientThread();
        });
    }

    private void GetRequest(Player player, NetworkRequest type, byte[] payload)
    {
        MemoryStream s = new MemoryStream(payload);
        BinaryReader reader = new BinaryReader(s);
        switch (type)
        {
            case NetworkRequest.Authentification:
                if (reader.ReadString() == NetworkConstants._authKey)
                {
                    SendRequest(player, NetworkRequest.AuthentificationSuccess);
                    // Send all player positions
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write((byte)NetworkRequest.CriticalError);
                    writer.Write("Wrong authentification key");
                }
                break;

            case NetworkRequest.PlayerInstantiate:
                _manager.SpawnPlayer(player, false, reader.ReadVector2(), reader.ReadVector2());
                SendToEveryone(NetworkRequest.PlayerInstantiate, payload, player.Id);
                break;
        }
    }

    private void SendRequest(Player p, NetworkRequest type)
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        switch (type)
        {
            case NetworkRequest.AuthentificationSuccess:
                writer.Write(p.Id);
                p.Tcp.SendRequest(NetworkRequest.AuthentificationSuccess, stream.ToArray());
                break;
        }
    }

    public void SendToEveryone(NetworkRequest type, byte[] payload, int except)
    {
        foreach (Player c in _manager.GetPlayers())
        {
            if (c.Id != except)
                c.Tcp.SendRequest(type, payload);
        }
    }

    private TcpListener _listener;

    private NetworkManager _manager;
    public void SpawnPlayer(Player p)
        => _manager.SpawnPlayer(p, true, Vector3.up, Vector3.zero);

    private byte _idCount;
}
