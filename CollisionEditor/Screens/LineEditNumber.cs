using System;
using Godot;

public partial class LineEditNumber : LineEditValidableBase
{
    private OpenTileMapScreen _screen;
    private const string StyleDirectory = "res://Styles/Textbox/Vector/";
    
    public override void _Ready()
    {
        base._Ready();
        _screen = OpenTileMapScreen.Screen;
        _screen.Parameters.TileNumberChangedEvents += OnTileNumberChanged;
        TextValidated += OnTextValidated;
    }
    
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out uint value) && value <= short.MaxValue;
    }
    
    protected override void LoadStyle()
    {
        StyleNormal = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_normal_offset.tres");
        StyleFocus = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_focus_offset.tres");
        StyleNormalError = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_normal_error_offset.tres");
        StyleFocusError = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_focus_error_offset.tres");
    }

    private void OnTextValidated(string text)
    {
        _screen.Parameters.TileNumber = int.Parse(Text);
    }

    private void OnTileNumberChanged()
    {
        if (int.TryParse(Text, out int value) && value == _screen.Parameters.TileNumber) return;
        Text = _screen.Parameters.TileNumber.ToString();
    }
}
