using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class HBoxContainerNumber : HBoxContainer
{
    public override void _Ready()
    {
        LoadTileMap.ExpertModeChangedEvents += isExpertMode => Visible = isExpertMode;
    }
}