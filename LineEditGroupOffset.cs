using Godot;

public partial class LineEditGroupOffset : LineEditValidableBase
{
    private const string StyleDirectory = "res://Styles/Textbox/Vector/";
    
    public override void _Ready()
    {
        base._Ready();
        SaveTileMapScreen.Parameters.GroupOffsetChangedEvents += OnGroupOffsetChanged;
        TextValidated += OnTextValidated;
    }
    
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out _);
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
        SaveTileMapScreen.Parameters.GroupOffset = int.Parse(Text);
    }

    private void OnGroupOffsetChanged()
    {
        if (int.TryParse(Text, out int value) && value == SaveTileMapScreen.Parameters.GroupOffset) return;
        Text = SaveTileMapScreen.Parameters.GroupOffset.ToString();
    }
}

