using Godot;

public partial class TextureRectTileMap : TextureRect
{
    public override void _Ready()
    {
        Texture = ImageTexture.CreateFromImage(LoadTileMap.Image);
    }
}
