using Godot;
using System;

public partial class TextEditByteAngle : TextEditValidable
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "0";

	public override void _Ready()
	{
		TextValidated += UpdateAngle;
		
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive =>
		{
			Text = isActive ? BaseText : string.Empty;
			Editable = isActive;
		};
	}

	protected override bool ValidateText()
	{
		return byte.TryParse(Text, out _);
	}

	private void UpdateAngle()
	{
		_screen.AngleMap.Angles[_screen.TileIndex] = byte.Parse(Text);
	}
}
