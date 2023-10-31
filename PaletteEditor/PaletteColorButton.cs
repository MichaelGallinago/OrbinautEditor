using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class PaletteColorButton : Button
{
    public Color CurrentColor { get; private set; }

    public void SetColor(Color color)
    {
        CurrentColor = color;
        SelfModulate = new Color(color.R, color.G, color.B);
    }
}