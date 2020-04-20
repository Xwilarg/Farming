using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkServer
{
    public NetworkServer(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();
        _clients = new List<Client>();
    }

    ~NetworkServer()
    { }

    public void CreateNewClientThread()
    {
        _clients.Add(new Client(this));
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
                    Debug.Log("Auth received");
                    if (reader.ReadString() == NetworkConstants._authKey)
                        writer.Write((byte)NetworkRequest.PlayerList);
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

    private TcpListener _listener;
    private List<Client> _clients;

    public class Client
    {
        public Client(NetworkServer s)
        {
            _thread = new Thread(new ThreadStart(() => { s.Listen(this); }));
            _thread.Start();
        }

        public TcpClient _client;
        public Thread _thread;
    }
}
