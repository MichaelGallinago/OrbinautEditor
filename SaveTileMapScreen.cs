using Godot;
using System;

public partial class SaveTileMapScreen : Control
{
    public static SaveTileMapScreen Object { get; private set; }
    public static SaveTilemapParameters Parameters { get; private set; } = new();
    public static bool IsSavePressed { get; set; }
    public static Image Image { set; get; }
    
    public SaveTileMapScreen()
    {
        Object = this;
        Parameters = new SaveTilemapParameters();
        IsSavePressed = false;
        Image = new Image();
    }

    public static SaveTilemapParameters GetParameters()
    {
        while (true)
        {
            if (IsSavePressed)
            {
                return Parameters;    
            }
        }
    }
}
