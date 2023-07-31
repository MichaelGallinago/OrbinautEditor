using Godot;

public partial class SaveTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => SaveTileMap.IsSavePressed = true;
    }
}
