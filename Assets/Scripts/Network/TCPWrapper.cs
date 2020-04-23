using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class TCPWrapper
{
    public TCPWrapper(TcpClient client, Action<Player, NetworkRequest, byte[]> callback)
    {
        _client = client;
        _callback = callback;
        _ns = _client.GetStream();
        _reader = new BinaryReader(_ns);
        _writer = new BinaryWriter(_ns);
        _thread = new Thread(new ThreadStart(Listen));
        _thread.Start();
    }

    ~TCPWrapper()
    {
        _client.Close();
    }

    private void Listen()
    {
        while (Thread.CurrentThread.IsAlive)
        {
            byte type = _reader.ReadByte();
            byte length = _reader.ReadByte();
            byte[] payload;
            if (length == 0)
                payload = new byte[0];
            else
            {
                payload = new byte[length];
                _reader.Read(payload, 0, length);
            }
            _callback(_parent, (NetworkRequest)type, payload);
        }
    }

    public void SendRequest(NetworkRequest type, byte[] payload)
    {
        byte[] data = new byte[payload.Length + 2];
        data[0] = (byte)type;
        data[1] = (byte)payload.Length;
        for (int i = 0; i < payload.Length; i++)
            data[i + 2] = payload[i];
        _writer.Write(data);
        _writer.Flush();
    }

    public void SetParent(Player parent)
        => _parent = parent;

    private TcpClient _client;
    private Action<Player, NetworkRequest, byte[]> _callback;
    private NetworkStream _ns;
    private BinaryReader _reader;
    private BinaryWriter _writer;
    private Thread _thread;
    private Player _parent;
}
