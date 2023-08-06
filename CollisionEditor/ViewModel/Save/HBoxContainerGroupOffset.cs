using Godot;

public partial class HBoxContainerGroupOffset : HBoxContainer
{
    public override void _Ready()
    {
        SaveTileMap.ExpertModeChangedEvents += isExpertMode => Visible = isExpertMode;
    }
}
