using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class TextureRectTileMapSave : TextureRect
{
    public override void _Ready()
    {
        SaveTileMap.TextureContainer = this;
    }
}