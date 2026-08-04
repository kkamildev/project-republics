
using System;

namespace project_republics.Utils.Helpers;

public static class StringHelper
{
    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;

        if (maxLength <= suffix.Length)
            return suffix[..maxLength];

        return string.Concat(value.AsSpan(0, maxLength - suffix.Length), suffix);
    }
}