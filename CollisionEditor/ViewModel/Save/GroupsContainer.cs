using Godot;

public partial class GroupsContainer : VBoxContainer
{
    public GroupsContainer()
    {
        SaveTileMap.GroupsContainer = this;
    }

    public override void _Ready()
    {
        ChildEnteredTree += SaveTileMap.AddGroup;
        ChildExitingTree += SaveTileMap.RemoveGroup;

        foreach (Node child in GetChildren())
        {
            SaveTileMap.AddGroup(child);
        }

        SaveTileMap.UpdateImage();
    }
}
