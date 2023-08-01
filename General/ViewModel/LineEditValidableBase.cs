using Godot;

public abstract partial class LineEditValidableBase : LineEditValidable
{
	private const string StyleDirectory = "res://Styles/Textbox/";
	protected override void LoadStyle()
	{
		StyleNormal = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_normal.tres");
		StyleFocus = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_focus.tres");
		StyleNormalError = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_normal_error.tres");
		StyleFocusError = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_focus_error.tres");
	}
}
