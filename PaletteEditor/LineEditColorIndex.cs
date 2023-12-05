using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class LineEditColorIndex : LineEdit
{
    private void OnSelectedColorChanged(Color color, int index)
    {
        Text = index < 0 ? string.Empty : index.ToString();
    }
}