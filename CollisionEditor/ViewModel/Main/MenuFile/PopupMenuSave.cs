using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class PopupMenuSave : PopupMenuHandler
{
    private const FileDialog.FileModeEnum FileMode = FileDialog.FileModeEnum.SaveFile;

    private static bool _isReady;
    
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
        CollisionEditor.FileDialog.VisibilityChanged += () =>
        {
            if (!CollisionEditor.FileDialog.Visible)
            {
                _isReady = true;
            }
        };
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

    private static async void OnAllPressed()
    {
        await Task.Run(WaitFileDialogClose);
        OnAngleMapPressed();
        await Task.Run(WaitFileDialogClose);
        OnHeightMapPressed();
        await Task.Run(WaitFileDialogClose);
        OnWidthMapPressed();
        await Task.Run(WaitFileDialogClose);
        OnTileMapPressed();
    }

    private static void WaitFileDialogClose()
    {
        while (true)
        {
            if (!_isReady) continue;
            _isReady = false;
            return;
        }
    }
    
    private static void OnAngleMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        CollisionEditor.OpenFileDialog(filters, FileMode, CollisionEditor.AngleMap.Save, 
            "Save AngleMap", "AngleMap.bin");
    }

    private static void OnHeightMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        CollisionEditor.OpenFileDialog(filters, FileMode, path => 
            TileUtilities.SaveCollisionMap(path, CollisionEditor.TileSet.Tiles, false),
            "Save HeightMap", "HeightMap.bin");
    }
    
    private static void OnWidthMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        CollisionEditor.OpenFileDialog(filters, FileMode, path => 
            TileUtilities.SaveCollisionMap(path, CollisionEditor.TileSet.Tiles, true), 
            "Save WidthMap", "WidthMap.bin");
    }

    private static async void OnTileMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.png", "PNG" }
        };
        
        Image tileMap = await CollisionEditor.Object.CreateTileMap();
        
        if (tileMap is null) return;
        
        CollisionEditor.OpenFileDialog(filters, FileMode, path => 
            TileUtilities.SaveTileMap(path, tileMap), "Save TileMap", "TileMap.png");
    }
}
