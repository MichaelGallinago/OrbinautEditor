using Godot;
using System;

public abstract partial class LineEditValidableVector : LineEditValidable
{
	private const string StyleDirectory = "res://Styles/Textbox/Vector/";
	
	private const float LetterWidth = 9f;
	private const int Offset = 20;
	protected OpenTileMapScreen Screen;

	protected LineEditValidableVector()
	{
		TextValidated += OnTextValidated;
		Resized += OnResized;
	}

	public override void _Ready()
	{
		base._Ready();
		Screen = OpenTileMapScreen.Screen;
		var label = GetChild<Label>(0);
		FocusEntered += () => label.Visible = false;
		FocusExited += () => label.Visible = true;
	}

	protected abstract void OnTextValidated(string text);

	protected override void LoadStyle()
	{
		StyleNormal = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_normal_offset.tres");
		StyleFocus = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_focus_offset.tres");
		StyleNormalError = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_normal_error_offset.tres");
		StyleFocusError = (StyleBoxFlat)ResourceLoader.Load($"{StyleDirectory}style_textbox_focus_error_offset.tres");
	}
	
	private void OnResized()
	{
		MaxLength = (int)((Size.X - Offset) / LetterWidth);
		if (Text.Length <= MaxLength) return;
		Text = Text[..MaxLength];
	}
}
