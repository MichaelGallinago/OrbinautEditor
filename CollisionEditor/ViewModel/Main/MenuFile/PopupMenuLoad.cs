using Godot;
using System.Collections.Generic;

public partial class PopupMenuLoad : PopupMenuHandler
{
    public PopupMenuLoad()
    {
        Name = "loadMenu";
        AddItem("TileMap", 0);
        AddItem("AngleMap", 1);
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
            
        CollisionEditor.OpenFileDialog(filters, FileDialog.FileModeEnum.OpenFile, 
            path => CollisionEditor.Object.CreateTileSet(path));
    }

    private void OnAngleMapPressed()
    {
        var filters = new Dictionary<string, string>
        {
            { "*.bin", "BIN" }
        };
        
        CollisionEditor.OpenFileDialog(filters, FileDialog.FileModeEnum.OpenFile, 
            CollisionEditor.CreateAngleMap);
    }
}
