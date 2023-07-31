using Godot;

public partial class SaveTileMap : Control
{
    private const byte ColorPickerIndex = 0;
    
    public static SaveTileMap Object { get; private set; }
    public static SaveTileMapParameters Parameters { get; private set; } = new();
    public static bool IsSavePressed { get; set; }
    public static Image Image { get; set; }
    public static GroupsContainer GroupsContainer { get; set; }
    
    public SaveTileMap()
    {
        Object = this;
        Parameters = new SaveTileMapParameters();
        IsSavePressed = false;
        Image = new Image();
        GroupsContainer = null;
    }

    public override void _Ready()
    {
        GroupsContainer.ChildEnteredTree += AddGroup;
        GroupsContainer.ChildExitingTree += RemoveGroup;
        
        AddGroup(GroupsContainer.GetChild(0));
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

    private static void AddGroup(Node group)
    {
        Parameters.Colors.Add(group.GetChild<ColorPicker>(ColorPickerIndex).Color);
    }
    
    private static void RemoveGroup(Node group)
    {
        Parameters.Colors.RemoveAt(group.GetIndex());
    }
}
