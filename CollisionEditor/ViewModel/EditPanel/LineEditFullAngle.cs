using Godot;
using System;
using System.Globalization;

public partial class LineEditFullAngle : LineEdit
{
	private const string BaseText = "360°";
	private const double ConvertByteToFull = 1.40625d;
	
	public override void _Ready()
	{
		CollisionEditorMain.ActivityChangedEvents += isActive => Text = isActive ? BaseText : string.Empty;
		CollisionEditorMain.AngleChangedEvents += OnAngleChanged;
	}
	
	private void OnAngleChanged(byte angle)
	{
		double value = Math.Round((byte.MaxValue + 1 - angle) * ConvertByteToFull, 2);
		Text = value.ToString(CultureInfo.InvariantCulture) + '°';
	}
}
