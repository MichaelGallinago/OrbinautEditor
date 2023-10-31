using Godot;

namespace OrbinautEditor.SurfaceEditor.ViewModel;

public partial class TextureRectBackground : TextureRect
{
    public override void _Ready()
    {
        SurfaceEditor.Background = this;
    }
}