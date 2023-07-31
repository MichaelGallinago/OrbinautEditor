using Godot;

public partial class TileNumberButtonAdd : Button
{
    public override void _Ready()
    {
        Pressed += () => OpenTileMapScreen.Parameters.TileNumber++;
    }
}
