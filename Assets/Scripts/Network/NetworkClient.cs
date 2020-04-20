using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkClient
{
    public NetworkClient(NetworkManager manager, string ip, int port)
    {
        _manager = manager;

        _client = new TcpClient(ip, port);
        _ns = _client.GetStream();
        _writer = new BinaryWriter(_ns);
        _reader = new BinaryReader(_ns);
        _listenThread = new Thread(new ThreadStart(Listen));
        _listenThread.Start();

        _writer.Write((byte)NetworkRequest.Authentification);
        _writer.Write(NetworkConstants._authKey);
        _writer.Flush();
    }

    ~NetworkClient()
    {
        _client.Close();
    }

    private void Listen()
    {
        while (Thread.CurrentThread.IsAlive)
        {
            byte type =_reader.ReadByte();
            switch ((NetworkRequest)type)
            {
                case NetworkRequest.CriticalError:
                    throw new System.Exception(_reader.ReadString());

                case NetworkRequest.AuthentificationSuccess:
                    _id = _reader.ReadByte();
                    _manager.SpawnPlayer(true, Vector3.up, Vector3.zero);
                    break;
            }
        }
    }

    private NetworkManager _manager;

    private TcpClient _client;
    private NetworkStream _ns;
    private BinaryWriter _writer;
    private BinaryReader _reader;

    private Thread _listenThread;

    private byte _id;
}
