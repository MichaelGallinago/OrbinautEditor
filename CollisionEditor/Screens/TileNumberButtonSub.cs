using Godot;

public partial class TileNumberButtonSub : Button
{
    public override void _Ready()
    {
        Pressed += () => OpenTileMapScreen.Parameters.TileNumber--;
    }
}
