using System;
using System.Globalization;

public class Angles
{
    const double ConvertByteToFull = 1.40625d;

    public static string GetHexAngle(byte byteAngle, byte maxLength)
    {
        return "0x" + $"{byteAngle:X}".PadLeft(maxLength, '0');
    }

    public static double GetFullAngle(byte byteAngle, bool round)
    {
        if (round)
        {
            return Math.Round((byte.MaxValue + 1 - byteAngle) * ConvertByteToFull, 2);
        }
        return (byte.MaxValue + 1 - byteAngle) * ConvertByteToFull;
    }

    public static byte GetByteAngle(string hexAngle, byte prefixLength)
    {
        return byte.Parse(hexAngle[prefixLength..], NumberStyles.HexNumber);
    }
}
