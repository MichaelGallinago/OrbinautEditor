using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class LoadTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => LoadTileMap.IsLoadPressed = true;
    }
}