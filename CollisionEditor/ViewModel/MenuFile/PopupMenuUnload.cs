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
        CollisionEditor.TileSet.Tiles.Clear();
        CollisionEditor.AngleMap.Angles.Clear();
        CollisionEditor.TileButtonsGrid.ClearTileButtons();
    }

    private void OnTileSetPressed()
    {
        CollisionEditor.ClearTiles();
    }
    
    private void OnAngleMapPressed()
    {
        CollisionEditor.ClearAngles();
    }
}
