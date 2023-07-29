using Godot;
using System;

public partial class TileNumberButtonSub : Button
{
    public override void _Ready()
    {
        OpenTileMapScreen screen = OpenTileMapScreen.Screen;
        Pressed += () => screen.TileNumber--;
    }
}
