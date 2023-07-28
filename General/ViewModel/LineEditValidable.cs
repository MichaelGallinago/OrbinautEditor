using Godot;
using System;

public abstract partial class LineEditValidable : LineEdit
{
	private StyleBoxFlat _styleNormal;
	private StyleBoxFlat _styleNormalError;
	private StyleBoxFlat _styleFocus;
	private StyleBoxFlat _styleFocusError;
	private bool _isTextValid = true;
	private string _text;

	private readonly TextChangedEventHandler _textUpdated;

	protected Action<string> TextValidated;

	protected abstract bool ValidateText();

	protected LineEditValidable()
	{
		_textUpdated += OnTextUpdated;
		_styleNormal = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_normal.tres");
		_styleFocus = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_focus.tres");
		_styleNormalError = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_normal_error.tres");
		_styleFocusError = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_focus_error.tres");
	}

	public override void _Process(double delta)
	{
		if (_text == Text) return;
		_textUpdated?.Invoke(Text);
		_text = Text;
	}

	private void UpdateStyle(bool isValid)
	{
		RemoveThemeStyleboxOverride("normal");
		RemoveThemeStyleboxOverride("focus");
		AddThemeStyleboxOverride("normal", isValid ? _styleNormal : _styleNormalError);
		AddThemeStyleboxOverride("focus", isValid ? _styleFocus : _styleFocusError);
	}

	private void OnTextUpdated(string text)
	{
		bool isValid = ValidateText();

		if (isValid)
		{
			TextValidated?.Invoke(text);
		}
		
		if (isValid == _isTextValid) return;
		
		_isTextValid = isValid;
		UpdateStyle(isValid || !Editable);
	}
}
