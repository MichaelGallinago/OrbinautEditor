using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class ExpertModeLoadTileMapButton : Button
{
    public override void _Ready()
    {
        Pressed += () => LoadTileMap.IsExpertMode = !LoadTileMap.IsExpertMode;
    }
}