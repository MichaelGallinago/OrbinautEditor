using System;
using Godot;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class TileUtilities
{
    private const byte DigitsNumber = 10;

    public static string GetCollisionValues(IEnumerable<byte> collisionArray)
    {
        StringBuilder builder = new();
        foreach (byte value in collisionArray)
        {
            builder.Append((char)((value < DigitsNumber ? '0' : 'A' - DigitsNumber) + value));
        }

        return string.Join(" ", builder.ToString().ToCharArray());
    }

    public static void SaveTileMap(string path, Image tileMap)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        if (tileMap.SavePng(path) != Error.Ok)
        {
            throw new Exception("TileMap");
        }
    }

    public static void SaveCollisionMap(string path, List<Tile> tiles, bool isWidths, BinaryFile.Types type)
    {
        if (path == null) return;
        
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using var writer = new BinaryWriter(File.Open(path, FileMode.CreateNew));
        {
            Tile blankTile = tiles.First();
            foreach (byte i in isWidths ? blankTile.Widths : blankTile.Heights)
            {
                writer.Write((byte)type);
            }
            
            foreach (byte value in tiles.Skip(1).SelectMany(tile => isWidths ? tile.Widths : tile.Heights))
            {
                writer.Write(value);
            }
        }
    }
}