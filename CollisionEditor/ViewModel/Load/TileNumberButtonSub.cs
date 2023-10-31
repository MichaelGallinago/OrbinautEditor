using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class TileNumberButtonSub : Button
{
    public override void _Ready()
    {
        Pressed += () => LoadTileMap.Parameters.TileNumber--;
    }
}