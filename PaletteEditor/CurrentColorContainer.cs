using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class CurrentColorContainer : VBoxContainer
{
    private void OnSelectedColorChanged(Color color, int index)
    {
        Visible = index >= 0;
    }
}