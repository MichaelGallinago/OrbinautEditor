using System;
using Godot;

public partial class SaveTileMap : Control
{
    private const byte ColorPickerIndex = 0;
    
    public static SaveTileMap Object { get; private set; }
    public static SaveTileMapParameters Parameters { get; private set; }
    public static bool? IsSavePressed { get; set; }
    public static GroupsContainer GroupsContainer { get; set; }
    public static TextureRectTileMapSave TextureContainer { get; set; }
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

    private static Image Image
    {
        get => TextureContainer.Texture.GetImage();
        set
        {
            if (value is null || IsSavePressed is not null) return;
            TextureContainer.Texture = ImageTexture.CreateFromImage(value);
            TextureContainer.QueueRedraw();
        }
    }

    public SaveTileMap()
    {
        Object = this;
        Parameters = new SaveTileMapParameters();
        IsSavePressed = null;
        Image = null;
        GroupsContainer = null;
        TextureContainer = null;
    }

    public override void _Ready()
    {
        Parameters.ColumnsChangedEvents += UpdateImage;
        Parameters.GroupOffsetChangedEvents += UpdateImage;
        IsExpertMode = false;
    }

    public static Image GetImage()
    {
        while (true)
        {
            if (IsSavePressed is not null)
            {
                return Image;    
            }
        }
    }

    public static async void UpdateImage()
    {
        if (IsSavePressed is false) return;
        Image = await CollisionEditor.TileSet.CreateTileMap(
            Object, Parameters.Columns, Parameters.Colors.ToArray(), 
            Parameters.GroupOffset, Parameters.Separation, Parameters.Offset);
    }

    public static void AddGroup(Node group)
    {
        Parameters.Colors.Add(group.GetChild<ColorPicker>(ColorPickerIndex).Color);
        UpdateImage();
    }
    
    public static void RemoveGroup(Node group)
    {
        Parameters.Colors.RemoveAt(group.GetIndex());
        UpdateImage();
    }
}
