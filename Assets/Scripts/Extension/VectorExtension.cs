using System.Runtime.CompilerServices;
using UnityEngine;

public static class Vector2IntExtension
{
    public static Vector2Int Modulo(this Vector2Int v1, int nb)
        => new Vector2Int(v1.x % nb, v1.y % nb);
}
