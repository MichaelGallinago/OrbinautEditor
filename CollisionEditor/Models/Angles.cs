using System;
using System.Globalization;

public class Angles
{
    public const int HexAngleMaxLength = 2;
    public const int HexAnglePrefixLength = 2;

    private const double ConvertByteToFull = 1.40625d;

    public byte ByteAngle { get; private set; }
    public string HexAngle { get; private set; } = "0x00";
    public double FullAngle { get; private set; }

    public static Angles FromHex(string hexAngle)
    {
        var angles = new Angles
        {
            ByteAngle = GetByteAngle(hexAngle),
            HexAngle = hexAngle
        };
        angles.FullAngle = GetFullAngle(angles.ByteAngle);

        return angles;
    }

    public static Angles FromByte(byte byteAngle)
    {
        return new Angles
        {
            ByteAngle = byteAngle,
            HexAngle = GetHexAngle(byteAngle),
            FullAngle = GetFullAngle(byteAngle)
        };
    }

    private static string GetHexAngle(byte byteAngle)
    {
        return "0x" + $"{byteAngle:X}".PadLeft(HexAngleMaxLength, '0');
    }

    private static double GetFullAngle(byte byteAngle)
    {
        return Math.Round((byte.MaxValue + 1 - byteAngle) * ConvertByteToFull, 1);
    }

    private static byte GetByteAngle(string hexAngle)
    {
        return byte.Parse(hexAngle[HexAnglePrefixLength..], NumberStyles.HexNumber);
    }
}
