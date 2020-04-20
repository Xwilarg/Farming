using System.IO;
using UnityEngine;

public static class Utils
{
    public static void Write(this BinaryWriter w, Vector2 pos)
    {
        w.Write(pos.x);
        w.Write(pos.y);
    }

    public static Vector2 ReadVector2(this BinaryReader r)
    {
        return new Vector2(r.ReadSingle(), r.ReadSingle());
    }
}
