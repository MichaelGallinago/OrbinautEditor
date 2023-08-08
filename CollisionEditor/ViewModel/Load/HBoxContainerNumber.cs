using Godot;

public partial class HBoxContainerNumber : HBoxContainer
{
    public override void _Ready()
    {
        LoadTileMap.ExpertModeChangedEvents += isExpertMode => Visible = isExpertMode;
    }
}
