using Godot;

public partial class AngleModeButton : Button
{
    public override void _Ready()
    {
        Pressed += () => CollisionEditor.IsTileMode = false;
    }
}
