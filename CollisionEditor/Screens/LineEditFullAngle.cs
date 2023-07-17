using Godot;
using System;
using System.Globalization;

public partial class LineEditFullAngle : LineEdit
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "360°";
	private const double ConvertByteToFull = 1.40625d;
	
	public override void _Ready()
	{
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive => Text = isActive ? BaseText : string.Empty;
		_screen.AngleChangedEvents += OnAngleChanged;
	}
	
	private void OnAngleChanged(byte angle)
	{
		double value = Math.Round((byte.MaxValue + 1 - angle) * ConvertByteToFull, 2);
		Text = value.ToString(CultureInfo.InvariantCulture) + '°';
	}
}
