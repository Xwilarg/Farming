using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class NetworkClient
{
    public NetworkClient(NetworkManager manager, string ip, int port)
    {
        _manager = manager;

        var me = new Player(new TCPWrapper(new TcpClient(ip, port), GetRequest), 255, true);
        _manager.SetMe(me);
        _manager.AddPlayer(me);
        SendRequest(me, NetworkRequest.Authentification);
    }

    private void GetRequest(Player me, NetworkRequest type, byte[] payload)
    {
        MemoryStream stream = new MemoryStream(payload);
        BinaryReader reader = new BinaryReader(stream);
        switch (type)
        {
            case NetworkRequest.CriticalError:
                throw new System.Exception(reader.ReadString());

            case NetworkRequest.AuthentificationSuccess:
                me.Id = reader.ReadByte();
                SendRequest(me, NetworkRequest.PlayerInstantiate);
                _manager.SpawnPlayer(me, true, Vector2.zero, Vector2Int.zero);
                break;

            case NetworkRequest.PlayerInstantiate:
                Player player = new Player(null, reader.ReadByte(), false);
                _manager.AddPlayer(player);
                _manager.SpawnPlayer(player, false, reader.ReadVector2(), reader.ReadVector2());
                break;

            case NetworkRequest.PlayerPosition:
                _manager.GetPlayer(reader.ReadByte()).Pc.UpdatePosition(reader.ReadVector2(), reader.ReadVector2());
                break;

            case NetworkRequest.ObjectInstantiate:
                var itemId = (ItemID)reader.ReadByte();
                var pos = reader.ReadVector2Int();
                var isItemOwner = reader.ReadBoolean();
                _manager.AddDelegateAction(() =>
                {
                    GameManager.MANAGER.InstantiateItem(ItemsList.Items.AllItems[itemId], pos, me, isItemOwner);
                });
                break;
        }
    }

    private void SendRequest(Player player, NetworkRequest type)
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);
        switch (type)
        {
            case NetworkRequest.Authentification:
                writer.Write(NetworkConstants._authKey);
                player.Tcp.SendRequest(NetworkRequest.Authentification, stream.ToArray());
                break;

            case NetworkRequest.PlayerInstantiate:
                writer.Write(_manager.GetMe().Id);
                writer.Write(Vector2.zero);
                writer.Write(Vector2.zero);
                player.Tcp.SendRequest(NetworkRequest.PlayerInstantiate, stream.ToArray());
                break;
        }
    }

    public void SendRequest(NetworkRequest type, byte[] payload)
        => _manager.GetMe().Tcp.SendRequest(type, payload);

    private NetworkManager _manager;
}
