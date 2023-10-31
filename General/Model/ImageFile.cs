using System.Collections.Generic;
using System.IO;
using Godot;

namespace OrbinautEditor.General.Model;

public static class ImageFile
{
    public static Dictionary<string, string> Filters { get; }
    
    static ImageFile()
    {
        Filters = new Dictionary<string, string>
        {
            { "*.png", "PNG" },
            { "*.bmp", "BMP" },
            { "*.jpg", "JPG" }
        };
    }
    
    public static Image Open(string path)
    {
        var image = new Image();
        if (image.Load(path) != Error.Ok)
        {
            throw new FileLoadException("TileSet");
        }
        return image;
    }
}