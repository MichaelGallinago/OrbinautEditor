using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class CancelSaveTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => SaveTileMap.IsSavePressed = false;
    }
}