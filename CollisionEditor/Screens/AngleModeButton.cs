using Godot;

public partial class AngleModeButton : Button
{
    public override void _Ready()
    {
        Pressed += () => CollisionEditorMain.IsTileMode = false;
    }
}
