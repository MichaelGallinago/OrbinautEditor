using System.Threading.Tasks;
using Godot;
using OrbinautEditor.CollisionEditor.Models;
using OrbinautEditor.General.Model;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.MenuFile;

public partial class PopupMenuSaveCollisions : General.ViewModel.PopupMenuHandler
{
    private const FileDialog.FileModeEnum FileMode = FileDialog.FileModeEnum.SaveFile;

    private static bool _isReady;
    
    public PopupMenuSaveCollisions()
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
        CollisionEditor.FileDialog.Open(BinaryFile.Filters, FileMode, CollisionEditor.AngleMap.Save, 
            "Save AngleMap", "AngleMap.bin");
    }

    private static void OnHeightMapPressed()
    {
        CollisionEditor.FileDialog.Open(BinaryFile.Filters, FileMode, path => 
            TileUtilities.SaveCollisionMap(path, CollisionEditor.TileSet.Tiles, false, 
                BinaryFile.Types.Heights), "Save HeightMap", "HeightMap.bin");
    }
    
    private static void OnWidthMapPressed()
    {
        CollisionEditor.FileDialog.Open(BinaryFile.Filters, FileMode, path => 
            TileUtilities.SaveCollisionMap(path, CollisionEditor.TileSet.Tiles, true, 
                BinaryFile.Types.Widths), "Save WidthMap", "WidthMap.bin");
    }

    private static async void OnTileMapPressed()
    {
        Image tileMap = await CollisionEditor.Object.CreateTileMap();
        
        if (tileMap is null) return;
        
        CollisionEditor.FileDialog.Open(ImageFile.Filters, FileMode, path => 
            TileUtilities.SaveTileMap(path, tileMap), "Save TileMap", "TileMap.png");
    }
}