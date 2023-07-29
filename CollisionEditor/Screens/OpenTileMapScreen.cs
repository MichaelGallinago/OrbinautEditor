using Godot;
using System;

public partial class OpenTileMapScreen : Control
{
    public static OpenTileMapScreen Screen { get; private set; }

    public Vector2I TileSize = new();
    public Vector2I Separation = new();
    public Vector2I Offset = new();
    
    public OpenTileMapScreen()
    {
        Screen = this;
    }
}
