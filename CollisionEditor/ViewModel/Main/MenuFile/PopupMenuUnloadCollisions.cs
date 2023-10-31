namespace OrbinautEditor.CollisionEditor.ViewModel.Main.MenuFile;

public partial class PopupMenuUnloadCollisions : General.ViewModel.PopupMenuHandler
{
    public PopupMenuUnloadCollisions()
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
            case 1: CollisionEditor.ClearTiles(); break;
            case 2: CollisionEditor.ClearAngles(); break;
        }
    }
    
    private static void OnAllPressed()
    {
        CollisionEditor.TileSet.Tiles.Clear();
        CollisionEditor.AngleMap.Angles.Clear();
        CollisionEditor.TileButtonsGrid.ClearTileButtons();
    }
}