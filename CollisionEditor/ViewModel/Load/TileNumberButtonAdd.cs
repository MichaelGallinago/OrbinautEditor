using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class TileNumberButtonAdd : Button
{
    public override void _Ready()
    {
        Pressed += () => LoadTileMap.Parameters.TileNumber++;
    }
}