using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.EditPanel;

public partial class TileModeButton : Button
{
    public override void _Ready()
    {
        Pressed += () => CollisionEditor.IsTileMode = true;
    }
}