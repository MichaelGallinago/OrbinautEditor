using Godot;
using System.IO;

public static class ImageLoader
{
    public static Image Load(string path)
    {
        var image = new Image();
        Error loadError = image.Load(path);
        if (loadError != Error.Ok)
        {
            throw new FileLoadException("TileSet");
        }
        return image;
    }
}
