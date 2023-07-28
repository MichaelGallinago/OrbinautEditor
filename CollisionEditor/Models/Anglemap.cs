using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AngleMap
{
    public const double ConvertRadiansToByte = 128 / Math.PI;

    public List<byte> Angles { get; private set; }

    public AngleMap(string binaryFilePath, int tileCount = 0)
    {
        //var reader = new BinaryReader(File.Open(path, FileMode.Open));
        //Angles = reader.ReadBytes((int)Math.Min(int.MaxValue, reader.BaseStream.Length)).ToList();
        Angles = File.ReadAllBytes(binaryFilePath).ToList();

        if (tileCount != 0)
        {
            SetAnglesCount(tileCount);
        }
    }

    public void SetAnglesCount(int tileCount)
    {
        if (Angles.Count >= tileCount)
        {
            Angles = Angles.GetRange(0, tileCount);
            return;
        }

        for (int i = tileCount - Angles.Count; i > 0; i--)
        {
            Angles.Add(0);
        }
    }

    public AngleMap(int tileCount = 0)
    {
        CreateAngles(tileCount);
    }

    public void CreateAngles(int count)
    {
        Angles = new List<byte>(new byte[count]);
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
            foreach (byte angle in Angles)
            {
                writer.Write(angle);
            }
        }
    }
    
    public void InsertAngle(int tileIndex)
    {
        Angles.Insert(tileIndex, 0);
    }

    public void RemoveAngle(int tileIndex)
    {
        Angles.RemoveAt(tileIndex);
    }
}
