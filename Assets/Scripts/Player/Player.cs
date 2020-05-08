public class Player
{
    public Player(TCPWrapper tcp, byte id, bool isLocalPlayer)
    {
        Tcp = tcp;
        Tcp?.SetParent(this);
        Id = id;
        Pc = null;
        Inventory = new Inventory();
        IsLocalPlayer = isLocalPlayer;
    }

    public static bool operator ==(Player w1, Player w2)
        => w1.Id == w2.Id;

    public static bool operator !=(Player w1, Player w2)
        => w1.Id != w2.Id;

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Player)obj);
    }

    public override int GetHashCode()
    {
        int hash = 13;
        return (hash * 7) + Id.GetHashCode();
    }

    public byte Id;
    public TCPWrapper Tcp;
    public PlayerController Pc;
    public Inventory Inventory;
    public bool IsLocalPlayer;
}
