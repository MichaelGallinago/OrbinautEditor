using Godot;

public partial class HBoxContainerTileSize : HBoxContainer
{
    public override void _Ready()
    {
        LoadTileMap.ExpertModeChangedEvents += isExpertMode => Visible = isExpertMode;
    }
}
