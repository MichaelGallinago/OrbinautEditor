using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AngleMap
{
    public List<byte> Angles { get; private set; }

    public AngleMap(string binaryFilePath, int tileCount = 0)
    {
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
        
        File.WriteAllBytes(path, Angles.ToArray());
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
