using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.EditPanel;

public class BigTileSquare
{
    public bool IsActive { get; set; }
    public Rect2 Rectangle;
    public Color Color;
    
    public BigTileSquare(Color color)
    {
        Rectangle = new Rect2();
        Color = color;
    }
}