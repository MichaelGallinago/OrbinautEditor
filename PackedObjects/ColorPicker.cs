using Godot;

namespace OrbinautEditor.PackedObjects;

public partial class ColorPicker : ColorPickerButton
{
    public override void _Ready()
    {
        ColorChanged += color =>
        {
            CollisionEditor.ViewModel.Save.SaveTileMap.Parameters.Colors[GetParent().GetIndex()] = color;
            CollisionEditor.ViewModel.Save.SaveTileMap.UpdateImage();
        };
    }
}