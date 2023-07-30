using Godot;
using System;

public partial class SaveTileMapScreen : Control
{
    public static SaveTileMapScreen Screen { get; private set; }
    public SaveTilemapParameters Parameters { get; }
    public bool IsSavePressed { get; set; }
    public Image Image { set; get; }
    
    
    public SaveTileMapScreen()
    {
        Parameters = new SaveTilemapParameters();
        Screen = this;
    }

    public SaveTilemapParameters GetParameters()
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
