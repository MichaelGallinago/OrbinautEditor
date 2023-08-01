using Godot;
using System;

public partial class TextureRectTileMapSave : TextureRect
{
    public override void _Ready()
    {
        SaveTileMap.TextureContainer = this;
    }
}
