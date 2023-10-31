using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class ExpertModeSaveTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => SaveTileMap.IsExpertMode = !SaveTileMap.IsExpertMode;
    }
}