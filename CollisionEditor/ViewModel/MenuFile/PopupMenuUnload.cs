using Godot;
using System;

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
            case 1: OnAngleMapPressed(); break;
            case 2: OnHeightMapPressed(); break;
        }
    }
    
    private static void OnAllPressed()
    {
        throw new NotImplementedException();
    }
    
    private static void OnAngleMapPressed()
    {
        throw new NotImplementedException();
    }
    
    private static void OnHeightMapPressed()
    {
        throw new NotImplementedException();
    }
}
