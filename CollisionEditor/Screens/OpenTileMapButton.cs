using Godot;

public partial class OpenTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => OpenTileMapScreen.IsLoadPressed = true;
    }
}
