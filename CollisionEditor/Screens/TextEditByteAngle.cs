using Godot;
using System;

public partial class TextEditByteAngle : TextEditValidable
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "0";

	public override void _Ready()
	{
		TextValidated += () => _screen.SetAngle(byte.Parse(Text));
		
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive =>
		{
			Text = isActive ? BaseText : string.Empty;
			Editable = isActive;
		};

		_screen.AngleChangedEvents += angle =>
		{
			var newText = angle.ToString();
			if (Text != newText)
			{
				Text = newText;
			}
		};
	}

	protected override bool ValidateText()
	{
		return byte.TryParse(Text, out _);
	}
}
