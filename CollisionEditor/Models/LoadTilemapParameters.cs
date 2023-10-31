using System;
using Godot;

namespace OrbinautEditor.CollisionEditor.Models;

public class LoadTileMapParameters
{
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
}