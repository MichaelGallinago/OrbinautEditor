using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class PalettePickerButton : ColorPickerButton
{
    private void OnSelectedColorChanged(Color color, int index)
    {
        if (index < 0) return;
        Color = color;
    }
}