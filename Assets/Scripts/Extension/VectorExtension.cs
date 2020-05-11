using UnityEngine;

public static class Vector2IntExtension
{
    public static Vector2Int Modulo(this Vector2Int v1, int nb)
        => new Vector2Int(v1.x % nb, v1.y % nb);

    public static Vector2Int ToVector2Int(this Vector3 v)
        => new Vector2Int((int)v.x, (int)v.z);

    public static Vector2Int Abs(this Vector2Int v)
        => new Vector2Int(Mathf.Abs(v.x), Mathf.Abs(v.y));
}
