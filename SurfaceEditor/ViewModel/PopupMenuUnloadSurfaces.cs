using Godot;
using System;

public partial class PopupMenuUnloadSurfaces : PopupMenuHandler
{
    public PopupMenuUnloadSurfaces()
    {
        Name = "unloadMenu";
        AddItem("All", 0); 
        AddItem("Surface", 1);
        AddItem("Background", 2);
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
