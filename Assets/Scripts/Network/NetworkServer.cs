using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkServer
{
    public NetworkServer(NetworkManager manager, int port)
    {
        _idCount = 0;
        _manager = manager;
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();
        _clients = new List<Client>();
        CreateNewClientThread();

        manager.SpawnPlayer(true, Vector3.up, Vector3.zero);
    }

    ~NetworkServer()
    { }

    public void CreateNewClientThread()
    {
        _clients.Add(new Client(this, _idCount));
        _idCount++;
    }

    public void Listen(Client c)
    {
        c._client = _listener.AcceptTcpClient();
        NetworkStream ns = c._client.GetStream();
        BinaryReader reader = new BinaryReader(ns);
        BinaryWriter writer = new BinaryWriter(ns);

        CreateNewClientThread(); // Once we accepted a client we create a new one to wait for the potentially next player

        while (Thread.CurrentThread.IsAlive && c._client.Connected)
        {
            byte type = reader.ReadByte();
            switch ((NetworkRequest)type)
            {
                case NetworkRequest.Authentification:
                    if (reader.ReadString() == NetworkConstants._authKey)
                    {
                        writer.Write((byte)NetworkRequest.AuthentificationSuccess);
                        writer.Write(c._id);
                    }
                    else
                    {
                        writer.Write((byte)NetworkRequest.CriticalError);
                        writer.Write("Wrong authentification key");
                    }
                    writer.Flush();
                    break;
            }
        }

        _clients.Remove(c);
    }

    public void SendToEveryone(NetworkRequest request, int except)
    {
        foreach (Client c in _clients.Take(_clients.Count - 1))
        {

        }
    }

    private TcpListener _listener;
    private List<Client> _clients;

    private NetworkManager _manager;

    private byte _idCount;

    public class Client
    {
        public Client(NetworkServer s, byte id)
        {
            _id = id;
            _thread = new Thread(new ThreadStart(() => { s.Listen(this); }));
            _thread.Start();
        }

        public byte _id;
        public TcpClient _client;
        public Thread _thread;
    }
}
