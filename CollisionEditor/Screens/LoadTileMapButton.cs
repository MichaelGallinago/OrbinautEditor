using Godot;

public partial class LoadTileMapButton : Button
{
    private OpenTileMapScreen _screen;
    
    public override void _Ready()
    {
        _screen = OpenTileMapScreen.Screen;
        Pressed += () => _screen.IsLoadPressed = true;
    }
}
