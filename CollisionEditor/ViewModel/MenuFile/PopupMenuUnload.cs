public partial class PopupMenuUnload : PopupMenuHandler
{
    public PopupMenuUnload()
    {
        Name = "unloadMenu";
        AddItem("All", 0); 
        AddItem("TileMap", 1);
        AddItem("AngleMap", 2);
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
        CollisionEditorMain.TileSet.Tiles.Clear();
        CollisionEditorMain.AngleMap.Angles.Clear();
        CollisionEditorMain.TileButtonsGrid.ClearTileButtons();
    }

    private void OnTileSetPressed()
    {
        CollisionEditorMain.ClearTiles();
    }
    
    private void OnAngleMapPressed()
    {
        CollisionEditorMain.ClearAngles();
    }
}
