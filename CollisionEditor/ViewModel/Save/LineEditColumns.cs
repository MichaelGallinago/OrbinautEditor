using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Save;

public partial class LineEditColumns : General.ViewModel.LineEditValidableBase
{
    private const string StyleDirectory = "res://Styles/Textbox/Vector/";
    
    public override void _Ready()
    {
        base._Ready();
        SaveTileMap.Parameters.ColumnsChangedEvents += OnColumnsChanged;
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
        SaveTileMap.Parameters.Columns = int.Parse(Text);
    }

    private void OnColumnsChanged()
    {
        if (int.TryParse(Text, out int value) && value == SaveTileMap.Parameters.Columns) return;
        Text = SaveTileMap.Parameters.Columns.ToString();
    }
}