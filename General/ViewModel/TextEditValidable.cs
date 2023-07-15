using Godot;
using System;

public abstract partial class TextEditValidable : TextEdit
{
	private StyleBoxFlat _styleNormal;
	private StyleBoxFlat _styleNormalError;
	private StyleBoxFlat _styleFocus;
	private StyleBoxFlat _styleFocusError;
	private bool _isTextValid = true;

	protected Action TextValidated;

	protected abstract bool ValidateText();

	protected TextEditValidable()
	{
		TextChanged += OnTextChanged;
		
		_styleNormal = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_normal.tres");
		_styleFocus = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_focus.tres");
		_styleNormalError = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_normal_error.tres");
		_styleFocusError = (StyleBoxFlat)ResourceLoader.Load("res://Styles/Textbox/style_textbox_focus_error.tres");
	}

	private void UpdateStyle(bool isValid)
	{
		RemoveThemeStyleboxOverride("normal");
		RemoveThemeStyleboxOverride("focus");
		AddThemeStyleboxOverride("normal", isValid ? _styleNormal : _styleNormalError);
		AddThemeStyleboxOverride("focus", isValid ? _styleFocus : _styleFocusError);
	}

	private void OnTextChanged()
	{
		bool isValid = ValidateText();

		if (isValid)
		{
			TextValidated?.Invoke();
		}
		
		if (isValid == _isTextValid) return;
		
		_isTextValid = isValid;
		UpdateStyle(isValid);
	}
}
