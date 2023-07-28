using Godot;
using System;
using System.Collections.Generic;

public partial class PopupMenuUnload : PopupMenuHandler
{
    private CollisionEditorMain _screen;
    
    public PopupMenuUnload()
    {
        Name = "unloadMenu";
        AddItem("All", 0); 
        AddItem("TileMap", 1);
        AddItem("AngleMap", 2);
    }

    public override void _Ready()
    {
        _screen = CollisionEditorMain.Screen;
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnAllPressed(); break;
            case 1: OnTileSetPressed(); break;
            case 2: OnAngleMapPressed(); break;
        }
    }
    
    private void OnAllPressed()
    {
        _screen.TileSet.Tiles.Clear();
        _screen.AngleMap.Angles.Clear();
        _screen.TileButtonsGrid.ClearTileButtons();
    }

    private void OnTileSetPressed()
    {
        _screen.ClearTiles();
    }
    
    private void OnAngleMapPressed()
    {
        _screen.ClearAngles();
    }
}
