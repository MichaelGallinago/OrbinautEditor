using Godot;
using System;
using System.Collections.Generic;

public class SaveTileMapParameters
{
    public Vector2I Separation = new();
    public Vector2I Offset = new();
    
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

    public List<Color> Colors { get; set; }
}
