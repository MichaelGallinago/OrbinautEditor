using System;
using Godot;

public partial class LoadTileMap : Control
{
    public static LoadTileMap Object { get; private set; }
    public static LoadTileMapParameters Parameters { private set; get; }
    public static bool? IsLoadPressed { get; set; }
    public static Image Image { set; get; }
    public static event Action<bool> ExpertModeChangedEvents;
    
    public static bool IsExpertMode
    {
        set
        {
            _isExpertMode = value;
            ExpertModeChangedEvents?.Invoke(value);
        }
        get => _isExpertMode;
    }

    private static bool _isExpertMode;
    
    
    public LoadTileMap()
    {
        Object = this;
        Parameters = new LoadTileMapParameters();
        IsLoadPressed = null;
        Image = new Image();
        ExpertModeChangedEvents = null;
    }

    public override void _Ready()
    {
        IsExpertMode = false;
    }

    public static LoadTileMapParameters GetParameters()
    {
        while (true)
        {
            if (IsLoadPressed is not null)
            {
                return Parameters;    
            }
        }
    }
}
