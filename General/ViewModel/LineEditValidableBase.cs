using Godot;
using System;

public abstract partial class LineEditValidableBase : LineEditValidable
{
	protected override void LoadStyle()
	{
		StyleNormal = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_normal.tres");
		StyleFocus = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_focus.tres");
		StyleNormalError = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_normal_error.tres");
		StyleFocusError = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_focus_error.tres");
	}
}
