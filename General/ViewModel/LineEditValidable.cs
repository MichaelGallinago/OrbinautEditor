using System;
using Godot;

namespace OrbinautEditor.General.ViewModel;

public abstract partial class LineEditValidable : LineEdit
{
	protected StyleBoxFlat StyleNormal;
	protected StyleBoxFlat StyleNormalError;
	protected StyleBoxFlat StyleFocus;
	protected StyleBoxFlat StyleFocusError;
	private bool _isTextValid = true;
	private string _text;

	private TextChangedEventHandler _textUpdated;

	protected Action<string> TextValidated;

	protected abstract bool ValidateText();

	public override void _Ready()
	{
		LoadStyle();
		_textUpdated += OnTextUpdated;
	}

	protected abstract void LoadStyle();

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
		AddThemeStyleboxOverride("normal", isValid ? StyleNormal : StyleNormalError);
		AddThemeStyleboxOverride("focus", isValid ? StyleFocus : StyleFocusError);
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