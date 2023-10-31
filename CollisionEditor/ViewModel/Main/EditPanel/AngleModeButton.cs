using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.EditPanel;

public partial class AngleModeButton : Button
{
    public override void _Ready()
    {
        Pressed += () => CollisionEditor.IsTileMode = false;
    }
}