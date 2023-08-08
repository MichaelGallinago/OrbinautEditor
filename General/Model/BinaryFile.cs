using System.Collections.Generic;
using System.IO;

public static class BinaryFile
{
    public const byte Metadata = 0;
    
    public static Dictionary<string, string> Filters { get; }

    public enum Types : byte
    {
        Angles, Heights, Widths
    }

    static BinaryFile()
    {
        Filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
    }

    public static bool TryOpen(string binaryFilePath, Types type, out byte[] fileData)
    {
        fileData = File.ReadAllBytes(binaryFilePath);
        return fileData[Metadata] == (byte)type;
    }

    public static Types Open(string binaryFilePath, out byte[] fileData)
    {
        fileData = File.ReadAllBytes(binaryFilePath);
        return (Types)fileData[Metadata];
    }
}
