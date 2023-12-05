using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace OrbinautEditor.PaletteEditor;

public partial class ColorButtonContainer : HBoxContainer
{
    [Signal] public delegate void SelectedColorChangedEventHandler(Color color, int index);
    
    private static readonly PackedScene PackedPaletteColorButton;

    private readonly System.Collections.Generic.Dictionary<Color, PaletteColorButton> _colorButtons = new();
    private Color _creationColor = Colors.Black;
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

    private void Clear()
    {
        foreach (PaletteColorButton button in _colorButtons.Values)
        {
            RemoveChild(button);
            button.QueueFree();
        }
        
        _colorButtons.Clear();
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
        if (!_colorButtons.TryAdd(color, button)) return;
        button.SetColor(color);
        button.Pressed += () => SelectColor(button.CurrentColor);
        AddChild(button);
    }

    private void SelectColor(Color? color)
    {
        _selectedColor = color;
        if (color == null)
        {
            EmitSignal(SignalName.SelectedColorChanged, Colors.Black, int.MinValue);
            return;
        }

        var selectedColor = (Color)color;
        EmitSignal(SignalName.SelectedColorChanged, selectedColor, _colorButtons[selectedColor].GetIndex());
    }

    private void OnRemoveColorButtonPressed()
    {
        if (_selectedColor == null) return;
        RemoveColor((Color)_selectedColor);
        SelectColor(null);
    }
    
    private void OnMoveLeftPressed()
    {
        if (_selectedColor == null) return;
        var color = (Color)_selectedColor;
        PaletteColorButton button = _colorButtons[color];
        MoveChild(button, Mathf.Max(button.GetIndex() - 1, 0));
        SelectColor(color);
    }
    
    private void OnMoveRightPressed()
    {
        if (_selectedColor == null) return;
        var color = (Color)_selectedColor;
        PaletteColorButton button = _colorButtons[color];
        MoveChild(button, Mathf.Min(button.GetIndex() + 1, GetChildCount() - 1));
        SelectColor(color);
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

    private void OnAddPaletteButtonPressed() => CreateColor(_creationColor);
    private void OnCreationColorChanged(Color color) => _creationColor = color;

    private void OnLoadPalette(string path)
    {
        Clear();
        
        Image image = Image.LoadFromFile(path);
        int sizeY = image.GetSize().Y;
        
        for (var y = 0; y < sizeY; y++)
        {
            CreateColor(image.GetPixel(0, y));
        }
    }
    
    private void OnLoadSprites(Array<string> paths)
    {
        foreach (string path in paths)
        {
            Image image = Image.LoadFromFile(path);
            Vector2I size = image.GetSize();
            var colors = new ConcurrentDictionary<Color, int>();

            Parallel.For(0, size.Y, y =>
            {
                for (var x = 0; x < size.X; x++)
                {
                    Color color = image.GetPixel(x, y);
                    if (color.A == 0f || colors.ContainsKey(color)) continue;
                    int index = x + y * size.X;
                    colors.AddOrUpdate(color, index, 
                        (_, oldIndex) => oldIndex <= index ? oldIndex : index);
                }
            });

            var sortedColors = colors.OrderBy(x => x.Value);
            foreach (var color in sortedColors)
            {
                CreateColor(color.Key);
            }
        }
    }
    
    private void OnSavePalette(string path)
    {
        var palette = Image.Create(1, _colorButtons.Count, false, Image.Format.Rgba8);

        var i = 0;
        foreach (Color color in _colorButtons.Keys)
        {
            palette.SetPixel(0, i++, color);
        }

        palette.SavePng(path);
    }
}
