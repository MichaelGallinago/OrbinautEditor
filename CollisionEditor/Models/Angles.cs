using System;
using System.Collections.Generic;
using System.Globalization;

public class Angles
{
    private const double ConvertByteToFull = 1.40625d;
    public static readonly IReadOnlyList<string> HexPrefixes = new[] { "0x", "0X", "0", "$" };

    public static string GetHexAngle(byte byteAngle, byte maxLength, byte prefixIndex)
    {
        return HexPrefixes[prefixIndex] + $"{byteAngle:X}".PadLeft(maxLength, '0');
    }

    public static double GetFullAngle(byte byteAngle, bool round)
    {
        double fullAngle = (byte.MaxValue + 1 - byteAngle) * ConvertByteToFull;
        return round ? Math.Round(fullAngle, 2) : fullAngle;
    }

    public static byte GetByteAngle(string hexAngle, byte prefixLength)
    {
        return byte.Parse(hexAngle[prefixLength..], NumberStyles.HexNumber);
    }
}
