using System.Collections.Generic;
using Godot;

namespace OrbinautEditor.PaletteEditor;

public partial class ColorButtonContainer : HBoxContainer
{
    [Signal] public delegate void SelectedColorChangedEventHandler(bool isNull, Color color);
    
    private static readonly PackedScene PackedPaletteColorButton;

    private readonly Dictionary<Color, PaletteColorButton> _colorButtons = new();
    private Color? _selectedColor;

    static ColorButtonContainer()
    {
        PackedPaletteColorButton = GD.Load<PackedScene>("res://PaletteEditor/PaletteColorButton.tscn");
    }

    public override void _Process(double delta)
    {
        if (_selectedColor == null || !Input.IsActionPressed("delete")) return;
        RemoveColor((Color)_selectedColor);
        SelectColor(null);
    }

    private void RemoveColor(Color color)
    {
        if (!_colorButtons.TryGetValue(color, out PaletteColorButton button)) return;
        _colorButtons.Remove(color);
        RemoveChild(button);
        button.QueueFree();
    }

    private void CreateColor(Color color)
    {
        var button = PackedPaletteColorButton.Instantiate<PaletteColorButton>();
        button.SetColor(color);
        button.Pressed += () => SelectColor(button.CurrentColor);
        _colorButtons.TryAdd(color, button);
        AddChild(button);
    }

    private void SelectColor(Color? color)
    {
        _selectedColor = color;
        bool isNull = _selectedColor == null;
        EmitSignal(SignalName.SelectedColorChanged, isNull, isNull ? Colors.Black : (Color)_selectedColor);
    }

    private void OnRemoveColorButtonPressed()
    {
        if (_selectedColor == null) return;
        RemoveColor((Color)_selectedColor);
        SelectColor(null);
    }

    private void OnSelectedColorChanged(Color color)
    {
        if (_selectedColor == null) return;

        PaletteColorButton button = _colorButtons[(Color)_selectedColor];
        button.SetColor(color);
        _colorButtons.Remove((Color)_selectedColor);

        bool isCanAdd = _colorButtons.TryAdd(color, button);

        if (isCanAdd)
        {
            SelectColor(color);
            return;
        }
        
        RemoveChild(button);
        button.QueueFree();
        SelectColor(null);
    }
}