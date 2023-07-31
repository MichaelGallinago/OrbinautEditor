using Godot;
using System.Collections.Generic;

public partial class PopupMenuSave : PopupMenuHandler
{
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
        
        CollisionEditorMain.OpenFileDialog(filters, FileMode, 
            path => CollisionEditorMain.AngleMap.Save(path));
    }
    
    private void OnHeightMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        CollisionEditorMain.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveCollisionMap(path, CollisionEditorMain.TileSet.Tiles, false));
    }
    
    private void OnWidthMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        CollisionEditorMain.OpenFileDialog(filters, FileMode, 
            path => TileUtilities.SaveCollisionMap(path, CollisionEditorMain.TileSet.Tiles, true));
    }

    private async void OnTileMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.png", "PNG" }
        };
        
        Image tileMap = await CollisionEditorMain.Object.CreateTileMap();
        CollisionEditorMain.OpenFileDialog(filters, FileMode, path => TileUtilities.SaveTileMap(path, tileMap));
    }
}
