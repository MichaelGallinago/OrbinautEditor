using Godot;
using System;
using System.Collections.Generic;

public class SaveTileMapParameters
{
    public List<Color> Colors { get; } = new();

    private Vector2I _separation;
    public Vector2I Separation
    {
        get => _separation;
        set
        {
            _separation = value;
            SaveTileMap.UpdateImage();
        }
    }

    private Vector2I _offset;
    public Vector2I Offset
    {
        get => _offset;
        set
        {
            _offset = value;
            SaveTileMap.UpdateImage();
        }
    }

    public event Action ColumnsChangedEvents;
    private int _columns;
    public int Columns
    {
        get => _columns;
        set
        {
            _columns = value;
            ColumnsChangedEvents?.Invoke();
        }
    }
    
    public event Action GroupOffsetChangedEvents;
    private int _groupOffset;
    public int GroupOffset
    {
        get => _groupOffset;
        set
        {
            _groupOffset = value;
            GroupOffsetChangedEvents?.Invoke();
        }
    }
}
