using Godot;

public partial class CancelSaveTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => SaveTileMap.IsSavePressed = false;
    }
}
