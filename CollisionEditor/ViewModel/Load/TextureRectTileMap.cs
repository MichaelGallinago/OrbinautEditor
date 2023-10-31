using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class TextureRectTileMap : TextureRect
{
    public override void _Ready()
    {
        Texture = ImageTexture.CreateFromImage(LoadTileMap.Image);
    }
}