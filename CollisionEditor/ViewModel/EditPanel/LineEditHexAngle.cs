using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;

public partial class LineEditHexAngle : LineEditValidableBase
{
	private CollisionEditorMain _screen;
	private const string BaseText = "0x00";
	private const int BasePrefixIndex = 0;
	private const byte BaseLength = 2;
	private readonly IReadOnlyList<string> _prefixes = new[] { "0x", "0X", "0", "$" };
	private int _prefixLength = 2;

	public override void _Ready()
	{
		_screen = CollisionEditorMain.Screen;
		_screen.ActivityChangedEvents += OnActivityChanged;
		_screen.AngleChangedEvents += OnAngleChanged;
		TextValidated += OnTextValidated;
	}

	protected override bool ValidateText()
	{
		var value = string.Empty;

		foreach (string prefix in _prefixes)
		{
			if (!Text.StartsWith(prefix)) continue;
			_prefixLength = prefix.Length;
			value = Text[_prefixLength..];
			break;
		}
		
		return byte.TryParse(value, NumberStyles.HexNumber, null, out _);
	}

	private void OnTextValidated(string text)
	{
		_screen.AngleChangedEvents -= OnAngleChanged;
		_screen.SetAngle(byte.Parse(text[_prefixLength..], NumberStyles.HexNumber, null));
		_screen.AngleChangedEvents += OnAngleChanged;
	}
	
	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
	}

	private void OnAngleChanged(byte angle)
	{
		Text = _prefixes[BasePrefixIndex] + $"{angle:X}".PadLeft(2, '0');
	}
}
