using UnityEngine;

public static class Vector2IntExtension
{
    public static Vector2Int Add(this Vector2Int v, int nb)
        => new Vector2Int(v.x + nb, v.y + nb);

    public static Vector2 Add(this Vector2 v, float nb)
        => new Vector2(v.x + nb, v.y + nb);

    public static Vector2Int Multiply(this Vector2Int v, int nb)
        => new Vector2Int(v.x * nb, v.y * nb);

    public static Vector2Int Abs(this Vector2Int v)
        => new Vector2Int(Mathf.Abs(v.x), Mathf.Abs(v.y));

    public static Vector2Int ToVector2Int(this Vector3 v)
        => new Vector2Int((int)v.x, (int)v.z);
}
