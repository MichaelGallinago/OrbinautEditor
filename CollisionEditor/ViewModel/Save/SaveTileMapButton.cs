using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class SaveTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => SaveTileMap.IsSavePressed = true;
    }
}