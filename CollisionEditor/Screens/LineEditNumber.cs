using Godot;

public partial class LineEditNumber : LineEditValidableBase
{
    private const string StyleDirectory = "res://Styles/Textbox/Vector/";
    
    public override void _Ready()
    {
        base._Ready();
        OpenTileMapScreen.Parameters.TileNumberChangedEvents += OnTileNumberChanged;
        TextValidated += OnTextValidated;
    }
    
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
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
        OpenTileMapScreen.Parameters.TileNumber = int.Parse(Text);
    }

    private void OnTileNumberChanged()
    {
        if (int.TryParse(Text, out int value) && value == OpenTileMapScreen.Parameters.TileNumber) return;
        Text = OpenTileMapScreen.Parameters.TileNumber.ToString();
    }
}
