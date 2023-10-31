using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class SaveParametersPanel : Panel
{
    private readonly Vector2 _expertHeight = new(0, 196);
    private readonly Vector2 _baseHeight = new(0, 168);
    
    public override void _Ready()
    {
        SaveTileMap.ExpertModeChangedEvents += isExpertMode => 
            CustomMinimumSize = isExpertMode ? _expertHeight : _baseHeight;
    }
}