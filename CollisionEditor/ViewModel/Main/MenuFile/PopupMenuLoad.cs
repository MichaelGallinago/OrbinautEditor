using Godot;

public partial class PopupMenuLoad : PopupMenuHandler
{
    public PopupMenuLoad()
    {
        Name = "loadMenu";
        AddItem("TileMap", 0);
        AddItem("AngleMap", 1);
        AddItem("HeightMap", 2);
        AddItem("WidthMap", 3);
    }

    protected override void OnItemPressed(long id)
    {
        switch (id)
        {
            case 0: OnTileMapPressed(); break;
            case 1: OnAngleMapPressed(); break;
            case 2: OnHeightMapPressed(); break;
            case 3: OnWidthMapPressed(); break;
        }
    }
    
    private static void OnTileMapPressed()
    {
        CollisionEditor.OpenFileDialog(ImageFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            CollisionEditor.Object.CreateTileSet, "Load TileMap", string.Empty);
    }

    private static void OnAngleMapPressed()
    {
        CollisionEditor.OpenFileDialog(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            CollisionEditor.OpenAngleMap, "Load AngleMap", string.Empty);
    }
    
    private static void OnHeightMapPressed()
    {
        CollisionEditor.OpenFileDialog(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            OnHeightMapSelected, "Load HeightMap", string.Empty);
    }
    
    private static void OnWidthMapPressed()
    {
        CollisionEditor.OpenFileDialog(BinaryFile.Filters, FileDialog.FileModeEnum.OpenFile, 
            OnWidthMapSelected, "Load WidthMap", string.Empty);
    }
    
    private static void OnHeightMapSelected(string filePath)
    {
        CollisionEditor.OpenTileSetFromCollisionMap(filePath, 
            BinaryFile.Types.Heights, TileSet.CreateFromHeights);
    }

    private static void OnWidthMapSelected(string filePath)
    {
        CollisionEditor.OpenTileSetFromCollisionMap(filePath, 
            BinaryFile.Types.Widths, TileSet.CreateFromWidths);
    }
}
