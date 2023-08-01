using Godot;

public partial class GroupsContainer : VBoxContainer
{
    public override void _Ready()
    {
        SaveTileMap.GroupsContainer = this;
    }
}
