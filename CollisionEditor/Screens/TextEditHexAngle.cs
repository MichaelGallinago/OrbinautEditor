using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;

public partial class TextEditHexAngle : TextEditValidable
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "0x00";
	private const int BasePrefixIndex = 0;
	private readonly IReadOnlyList<string> _prefixes = new[] { "0x", "0X", "0", "$" };
	private int _prefixLength = 2;

	public override void _Ready()
	{
		TextValidated += UpdateAngle;
		
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive =>
		{
			Text = isActive ? BaseText : string.Empty;
			Editable = isActive;
		};
		
		_screen.AngleChangedEvents += angle =>
		{
			var newText = $"{_prefixes[BasePrefixIndex]}{angle:X}";
			if (Text != newText)
			{
				Text = newText;
			}
		};
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

	private void UpdateAngle()
	{
		_screen.SetAngle(byte.Parse(Text[_prefixLength..], NumberStyles.HexNumber, null));
	}
}
