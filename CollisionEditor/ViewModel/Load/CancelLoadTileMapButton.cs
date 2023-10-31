using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class CancelLoadTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => LoadTileMap.IsLoadPressed = false;
    }
}