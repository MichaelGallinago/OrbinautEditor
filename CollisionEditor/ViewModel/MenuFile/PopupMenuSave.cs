using Godot;
using System;
using System.Collections.Generic;

public partial class PopupMenuSave : PopupMenuHandler
{
    private CollisionEditorMainScreen screen;
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
        screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
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
        
        screen.OpenFileDialog(filters, FileMode, 
            path => screen.AngleMap.Save(path));
    }
    
    private void OnHeightMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        screen.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveCollisionMap(path, screen.TileSet.Tiles, false));
    }
    
    private void OnWidthMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        screen.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveCollisionMap(path, screen.TileSet.Tiles, true));
    }

    private async void OnTileMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.png", "PNG" }
        };

        // TODO: no more constants
        Image tileMap = await screen.TileSet.CreateTileMap(
            8, new Color[] { Colors.Yellow, Colors.Black, Colors.White },
            new[] { 6, 6, 0 }, new Vector2I(2, 2), new Vector2I());

        screen.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveTileMap(path, tileMap));
    }
}
