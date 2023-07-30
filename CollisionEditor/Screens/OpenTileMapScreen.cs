using Godot;
using System;
using System.Threading.Tasks;

public partial class OpenTileMapScreen : Control
{
    public static OpenTileMapScreen Screen { get; private set; }
    public OpenTilemapParameters Parameters { get; }
    public bool IsLoadPressed { get; set; }
    public Image Image { set; get; }
    
    
    public OpenTileMapScreen()
    {
        Parameters = new OpenTilemapParameters();
        Screen = this;
    }

    public OpenTilemapParameters GetParameters()
    {
        while (true)
        {
            if (IsLoadPressed)
            {
                return Parameters;    
            }
        }
    }
}
