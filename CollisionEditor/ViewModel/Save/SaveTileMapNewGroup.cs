using Godot;

public partial class SaveTileMapNewGroup : Button
{
    private PackedScene _packedGroup;

    public override void _Ready()
    {
        _packedGroup = GD.Load<PackedScene>("res://PackedObjects/color_picker_container.tscn");
        Pressed += () => SaveTileMap.GroupsContainer.AddChild(_packedGroup.Instantiate());
    }
}
