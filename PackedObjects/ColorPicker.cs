using Godot;

public partial class ColorPicker : ColorPickerButton
{
    public override void _Ready()
    {
        ColorChanged += color =>
        {
            SaveTileMap.Parameters.Colors[GetParent().GetIndex()] = color;
            SaveTileMap.UpdateImage();
        };
    }
}
