using Godot;
using System;

public partial class LineEditByteAngle : LineEditValidable
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "0";

	public override void _Ready()
	{
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += OnActivityChanged;
		_screen.AngleChangedEvents += OnAngleChanged;
		TextValidated += OnTextValidated;
	}

	protected override bool ValidateText()
	{
		return byte.TryParse(Text, out _);
	}
	
	private void OnTextValidated(string text)
	{
		_screen.AngleChangedEvents -= OnAngleChanged;
		_screen.SetAngle(byte.Parse(text));
		_screen.AngleChangedEvents += OnAngleChanged;
	}

	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
	}

	private void OnAngleChanged(byte angle)
	{
		Text = angle.ToString();
	}
}
