using Godot;
using System;
using System.Collections.Generic;

public partial class PopupMenuUnload : PopupMenuHandler
{
    private CollisionEditorMainScreen screen;
    
    public PopupMenuUnload()
    {
        Name = "unloadMenu";
        AddItem("All", 0); 
        AddItem("TileMap", 1);
        AddItem("AngleMap", 2);
    }

    public override void _Ready()
    {
        screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnAllPressed(); break;
            case 1: OnAngleMapPressed(); break;
            case 2: OnTileSetPressed(); break;
        }
    }
    
    private void OnAllPressed()
    {
        screen.TileSet.Tiles.Clear();
        screen.AngleMap.Angles.Clear();
    }
    
    private void OnAngleMapPressed()
    {
        screen.AngleMap.UnloadAngles();
    }
    
    private void OnTileSetPressed()
    {
        screen.TileSet.UnloadTiles();
    }
}
