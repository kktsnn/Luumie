using System.Numerics;

namespace LuumieEngine.Structs;

public struct Vector2Int
{
    public int X;
    public int Y;

    public Vector2Int(int value) : this(value, value)
    {
    }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2 ToVector2() => new(X, Y);
    public int SqrMagnitude() => X * X + Y * Y;

    public static Vector2Int Zero => new(0, 0);
    public static Vector2Int Up => new(0, 1);
    public static Vector2Int Down => new(0, -1);
    public static Vector2Int Left => new(-1, 0);
    public static Vector2Int Right => new(1, 0);
    public static Vector2Int One => new(1, 1);
    
    public static Vector2Int operator +(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X + v2.X, v1.Y + v2.Y );
    }
    
    public static Vector2Int operator -(Vector2Int v1, Vector2Int v2)
    {
        return new Vector2Int(v1.X - v2.X, v1.Y - v2.Y);
    }

    public static Vector2Int operator *(Vector2Int v, int s)
    {
        return new Vector2Int(v.X * s, v.Y * s);
    }

    public static Vector2Int operator /(Vector2Int v, int s)
    {
        return new Vector2Int(v.X / s, v.Y = s);
    }

    public static bool operator ==(Vector2Int v1, Vector2Int v2)
    {
        return v1.X == v2.X && v1.Y == v2.Y;
    }

    public static bool operator !=(Vector2Int v1, Vector2Int v2)
    {
        return !(v1 == v2);
    }
}

public static class Vector2Ext
{
    public static Vector2Int ToVector2Int(this Vector2 v)
        => new((int)v.X, (int)v.Y);
}