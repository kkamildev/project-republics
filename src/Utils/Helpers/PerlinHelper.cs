

using System;

namespace project_republics.Utils.Helpers;

public static class PerlinHelper
{
    public static int GenerateSeed(int length)
    {
        return Guid.NewGuid().ToString("N")[..length].GetHashCode();
    }
}