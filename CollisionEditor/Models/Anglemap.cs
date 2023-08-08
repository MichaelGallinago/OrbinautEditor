using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public class AngleMap
{
    public List<byte> Angles { get; private set; }

    public AngleMap(IEnumerable<byte> angles, int tileCount = 0)
    {
        Angles = angles.ToList();

        if (tileCount != 0)
        {
            SetAnglesCount(tileCount);
        }
    }
    
    public AngleMap(int tileCount = 0)
    {
        CreateAngles(tileCount);
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

        byte[] fileData = Angles.ToArray();
        fileData[BinaryFile.Metadata] = (byte)BinaryFile.Types.Angles;
        File.WriteAllBytes(path, fileData);
    }
    
    public void InsertAngle(int tileIndex)
    {
        Angles.Insert(tileIndex, 0);
    }

    public void RemoveAngle(int tileIndex)
    {
        Angles.RemoveAt(tileIndex);
    }

    public void MoveAngle(int fromTileIndex, int toTileIndex)
    {
        byte angle = Angles[fromTileIndex];
        Angles.RemoveAt(fromTileIndex);
        Angles.Insert(toTileIndex, angle);
    }
}
