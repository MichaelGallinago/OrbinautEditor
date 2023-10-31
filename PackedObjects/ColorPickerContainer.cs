using Godot;

namespace OrbinautEditor.PackedObjects;

public partial class ColorPickerContainer : HBoxContainer
{
    [Export] private Color _baseColor;

    public override void _Ready()
    {
        GetChild<ColorPicker>(0).Color = _baseColor;
    }
}