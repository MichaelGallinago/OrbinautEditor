using Godot;

public partial class TextureRectSurface : TextureRect
{
    public override void _Ready()
    {
        SurfaceEditor.Surface = this;
    }
}
