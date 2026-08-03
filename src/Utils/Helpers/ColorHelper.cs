
using System.Globalization;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Helpers;

public static class ColorHelper
{
    public static string ToHex(Color color, bool includeAlpha = false)
    {
        if (includeAlpha)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
        }
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    public static Color FromHex(string hex)
    {
        if (hex.StartsWith('#')) hex = hex[1..];
        
        if (hex.Length == 3 || hex.Length == 4)
        {
            string r = hex[0].ToString() + hex[0];
            string g = hex[1].ToString() + hex[1];
            string b = hex[2].ToString() + hex[2];
            string a = hex.Length == 4 ? hex[3].ToString() + hex[3] : "FF";
            hex = r + g + b + a;
        }

        if (hex.Length == 6) hex += "FF";
        
        if (hex.Length != 8) 
            throw new System.ArgumentException("Invalid hex format. Use #RGB, #RGBA, #RRGGBB, or #RRGGBBAA");

        byte rByte = byte.Parse(hex[..2], NumberStyles.HexNumber);
        byte gByte = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte bByte = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        byte aByte = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);

        return new Color(rByte, gByte, bByte, aByte);
    }
}