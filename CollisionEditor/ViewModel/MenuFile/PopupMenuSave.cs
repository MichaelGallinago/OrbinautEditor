using Godot;
using System;

public partial class PopupMenuSave : PopupMenuHandler
{
    public PopupMenuSave()
    {
        Name = "saveMenu";
        AddItem("All", 0);
        AddItem("AngleMap", 1);
        AddItem("HeightMap", 2);
        AddItem("WidthMap", 3);
        AddItem("TileMap", 4);
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
    
    private static void OnWidthMapPressed()
    {
        throw new NotImplementedException();
    }
    
    private static void OnTileMapPressed()
    {
        throw new NotImplementedException();
    }
}
