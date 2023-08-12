using Godot;

public partial class TextureRectBackground : TextureRect
{
    public override void _Ready()
    {
        SurfaceEditor.Background = this;
    }
}
