using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class TCPWrapper
{
    public TCPWrapper(TcpClient client, Action<TCPWrapper, NetworkRequest, byte[]> callback, byte id)
    {
        _client = client;
        _callback = callback;
        _ns = _client.GetStream();
        _reader = new BinaryReader(_ns);
        _writer = new BinaryWriter(_ns);
        _thread = new Thread(new ThreadStart(Listen));
        _thread.Start();
        _id = id;
    }

    ~TCPWrapper()
    {
        _client.Close();
    }

    public static bool operator==(TCPWrapper w1, TCPWrapper w2)
        => w1._id == w2._id;

    public static bool operator !=(TCPWrapper w1, TCPWrapper w2)
        => w1._id != w2._id;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((TCPWrapper)obj);
    }

    public override int GetHashCode()
    {
        int hash = 13;
        return (hash * 7) + _id.GetHashCode();
    }

    private void Listen()
    {
        while (Thread.CurrentThread.IsAlive)
        {
            byte type = _reader.ReadByte();
            byte length = _reader.ReadByte();
            byte[] payload = new byte[length];
            _reader.Read(payload, 0, length);
            _callback(this, (NetworkRequest)type, payload);
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

    private TcpClient _client;
    private Action<TCPWrapper, NetworkRequest, byte[]> _callback;
    private NetworkStream _ns;
    private BinaryReader _reader;
    private BinaryWriter _writer;
    private Thread _thread;
    private byte _id; public int GetId() => _id; public void SetId(byte value) => _id = value;
}
