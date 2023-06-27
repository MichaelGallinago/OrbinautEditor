using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AngleMap
{
    private const double ConvertRadiansToByte = 128 / Math.PI;

    public List<byte> Values { get; private set; }

    public AngleMap(string path)
    {
        var reader = new BinaryReader(File.Open(path, FileMode.Open));
        Values = reader.ReadBytes((int)Math.Min(int.MaxValue, reader.BaseStream.Length)).ToList();
    }

    public AngleMap(int tileCount = 0)
    {
        Values = new List<byte>(new byte[tileCount]);
    }

    public void Save(string path)
    {
        if (path == null) return;
        
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
        using BinaryWriter writer = new(File.Open(path, FileMode.CreateNew));
        {
            foreach (byte value in Values)
            {
                writer.Write(value);
            }
        }
    }

    public byte SetAngleFromLine(int tileIndex, Vector2I positionGreen, Vector2I positionBlue)
    {
        return Values[tileIndex] = (byte)(Math.Atan2(
            positionBlue.Y - positionGreen.Y,
            positionBlue.X - positionGreen.X)
            * ConvertRadiansToByte);
    }

    public byte SetAngle(int tileIndex, byte value)
    {
        return Values[tileIndex] = value;
    }

    public byte ChangeAngle(int tileIndex, int value)
    {
        return Values[tileIndex] = (byte)(Values[tileIndex] + value);
    }

    public void InsertAngle(int tileIndex)
    {
        Values.Insert(tileIndex, 0);
    }

    public void RemoveAngle(int tileIndex)
    {
        Values.RemoveAt(tileIndex);
    }
}
