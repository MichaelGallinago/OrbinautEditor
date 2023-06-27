using Godot;
using System;

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
    
    private static void OnTileMapPressed()
    {
        throw new NotImplementedException();
    }
    
    private static void OnAngleMapPressed()
    {
        throw new NotImplementedException();
    }
}
