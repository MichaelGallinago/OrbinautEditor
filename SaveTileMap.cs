using Godot;

public partial class SaveTileMap : Control
{
    public static SaveTileMap Object { get; private set; }
    public static SaveTileMapParameters Parameters { get; private set; } = new();
    public static bool IsSavePressed { get; set; }
    public static Image Image { set; get; }
    
    public SaveTileMap()
    {
        Object = this;
        Parameters = new SaveTileMapParameters();
        IsSavePressed = false;
        Image = new Image();
    }

    public static SaveTileMapParameters GetParameters()
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
