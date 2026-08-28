

using System;

public struct ByteVector2
{
    public byte X;
    public byte Y;

    public ByteVector2(byte x, byte y)
    {
        X = x;
        Y = y;
    }

    public static ByteVector2 operator +(ByteVector2 a, ByteVector2 b) => new((byte)(a.X + b.X), (byte)(a.Y + b.Y));

    public static ByteVector2 operator -(ByteVector2 a, ByteVector2 b) => new((byte)(a.X - b.X), (byte)(a.Y - b.Y));

    public static ByteVector2 operator *(ByteVector2 a, byte s) => new((byte)(a.X * s), (byte)(a.Y * s));

    public static ByteVector2 operator /(ByteVector2 a, byte s) => new((byte)(a.X / s), (byte)(a.Y / s));


    public static ByteVector2 operator +(ByteVector2 a, byte s) => new((byte)(a.X + s), (byte)(a.Y + s));

    public static ByteVector2 operator -(ByteVector2 a, byte s) => new((byte)(a.X - s), (byte)(a.Y - s));


    public static bool operator ==(ByteVector2 a, ByteVector2 b) => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(ByteVector2 a, ByteVector2 b) => !(a == b);

    public override readonly bool Equals(object obj) => obj is ByteVector2 v && this == v;

    public override readonly int GetHashCode() => HashCode.Combine(X, Y);


    public int LengthSquared() => X * X + Y * Y;

    public float Length() => MathF.Sqrt(LengthSquared());

    public ByteVector2 Clamp(byte min, byte max) => new(Math.Clamp(X, min, max), Math.Clamp(Y, min, max));

    public override string ToString() => $"({X}, {Y})";
}
