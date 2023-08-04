using System.Collections;
using Godot;

public partial class SaveTileMap : Control
{
    private const byte ColorPickerIndex = 0;
    
    public static SaveTileMap Object { get; private set; }
    public static SaveTileMapParameters Parameters { get; private set; }
    public static bool? IsSavePressed { get; set; }
    public static GroupsContainer GroupsContainer { get; set; }
    public static TextureRectTileMapSave TextureContainer { get; set; }

    private static Image Image
    {
        get => TextureContainer.Texture.GetImage();
        set
        {
            if (value is null) return;
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
        GroupsContainer.ChildEnteredTree += AddGroup;
        GroupsContainer.ChildExitingTree += RemoveGroup;

        Parameters.ColumnsChangedEvents += UpdateImage;
        Parameters.GroupOffsetChangedEvents += UpdateImage;
        
        AddGroup(GroupsContainer.GetChild(0));
        UpdateImage();
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

    private static void AddGroup(Node group)
    {
        Parameters.Colors.Add(group.GetChild<ColorPicker>(ColorPickerIndex).Color);
        UpdateImage();
    }
    
    private static void RemoveGroup(Node group)
    {
        Parameters.Colors.RemoveAt(group.GetIndex());
        UpdateImage();
    }
}
