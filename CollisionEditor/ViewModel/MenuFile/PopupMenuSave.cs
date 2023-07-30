using Godot;
using System;
using System.Collections.Generic;

public partial class PopupMenuSave : PopupMenuHandler
{
    private CollisionEditorMain _screen;
    private const FileDialog.FileModeEnum FileMode = FileDialog.FileModeEnum.SaveFile;
    
    public PopupMenuSave()
    {
        Name = "saveMenu";
        AddItem("All", 0);
        AddItem("AngleMap", 1);
        AddItem("HeightMap", 2);
        AddItem("WidthMap", 3);
        AddItem("TileMap", 4);
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
            case 1: OnAngleMapPressed(); break;
            case 2: OnHeightMapPressed(); break;
            case 3: OnWidthMapPressed(); break;
            case 4: OnTileMapPressed(); break;
        }
    }

    private void OnAllPressed()
    {
        OnAngleMapPressed();
        OnHeightMapPressed();
        OnWidthMapPressed();
        OnTileMapPressed();
    }
    
    private void OnAngleMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        _screen.OpenFileDialog(filters, FileMode, 
            path => _screen.AngleMap.Save(path));
    }
    
    private void OnHeightMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        _screen.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveCollisionMap(path, _screen.TileSet.Tiles, false));
    }
    
    private void OnWidthMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        _screen.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveCollisionMap(path, _screen.TileSet.Tiles, true));
    }

    private async void OnTileMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.png", "PNG" }
        };
        
        Image tileMap = await _screen.CreateTileMap();
        _screen.OpenFileDialog(filters, FileMode, path => TileUtilities.SaveTileMap(path, tileMap));
    }
}
