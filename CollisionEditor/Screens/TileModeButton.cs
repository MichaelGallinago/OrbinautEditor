using Godot;

public partial class TileModeButton : Button
{
    public override void _Ready()
    {
        Pressed += () => CollisionEditor.IsTileMode = true;
    }
}
