using Godot;

public partial class OpenTileMapScreen : Control
{
    public static OpenTileMapScreen Object { get; private set; }
    public static OpenTilemapParameters Parameters { private set; get; }
    public static bool IsLoadPressed { get; set; }
    public static Image Image { set; get; }
    
    
    public OpenTileMapScreen()
    {
        Object = this;
        Parameters = new OpenTilemapParameters();
        IsLoadPressed = false;
        Image = new Image();
    }

    public static OpenTilemapParameters GetParameters()
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
