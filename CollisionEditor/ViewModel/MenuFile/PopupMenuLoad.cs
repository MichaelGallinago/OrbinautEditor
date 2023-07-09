using Godot;
using System;
using System.Collections.Generic;

public partial class PopupMenuLoad : PopupMenuHandler
{
    private CollisionEditorMainScreen _screen;

    public PopupMenuLoad()
    {
        Name = "loadMenu";
        AddItem("TileMap", 0);
        AddItem("AngleMap", 1);
    }
    
    public override void _Ready()
    {
        _screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
    }
    
    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnTileMapPressed(); break;
            case 1: OnAngleMapPressed(); break;
        }
    }
    
    private void OnTileMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.png", "PNG" }
        };
            
        _screen.OpenFileDialog(filters, FileDialog.FileModeEnum.OpenFile, 
            path => _screen.CreateTileSet(path));
    }

    private void OnAngleMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        _screen.OpenFileDialog(filters, FileDialog.FileModeEnum.OpenFile, 
            path => _screen.CreateAngleMap(path));
    }
}
