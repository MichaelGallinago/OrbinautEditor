using Godot;

public partial class LoadTileMap : Control
{
    public static LoadTileMap Object { get; private set; }
    public static LoadTileMapParameters Parameters { private set; get; }
    public static bool IsLoadPressed { get; set; }
    public static Image Image { set; get; }
    
    
    public LoadTileMap()
    {
        Object = this;
        Parameters = new LoadTileMapParameters();
        IsLoadPressed = false;
        Image = new Image();
    }

    public static LoadTileMapParameters GetParameters()
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
