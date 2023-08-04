using Godot;

public partial class CancelLoadTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => LoadTileMap.IsLoadPressed = false;
    }
}
