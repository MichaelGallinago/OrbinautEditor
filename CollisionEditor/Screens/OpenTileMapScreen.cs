using Godot;
using System;

public partial class OpenTileMapScreen : Control
{
    public static OpenTileMapScreen Screen { get; private set; }

    public Vector2I TileSize = new();
    public Vector2I Separation = new();
    public Vector2I Offset = new();

    private int _tileNumber;
    public int TileNumber
    {
        get => _tileNumber;
        set
        {
            _tileNumber = value; 
            TileNumberChangedEvents?.Invoke();
        }
    }
    
    public event Action TileNumberChangedEvents;
    
    public OpenTileMapScreen()
    {
        Screen = this;
    }
}
