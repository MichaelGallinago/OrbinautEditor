using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class PalettePickerButton : ColorPickerButton
{
    private void OnSelectedColorChanged(bool isNull, Color color)
    {
        if (isNull) return;
        Color = color;
    }
}