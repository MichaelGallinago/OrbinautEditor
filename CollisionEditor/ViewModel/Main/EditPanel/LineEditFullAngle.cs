using System.Globalization;
using Godot;
using OrbinautEditor.CollisionEditor.Models;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.EditPanel;

public partial class LineEditFullAngle : LineEdit
{
	private const string BaseText = "360°";
	private const char Postfix = '°';
	
	public override void _Ready()
	{
		CollisionEditor.ActivityChangedEvents += isActive => Text = isActive ? BaseText : string.Empty;
		CollisionEditor.AngleChangedEvents += OnAngleChanged;
	}
	
	private void OnAngleChanged(byte angle)
	{
		Text = Angles.GetFullAngle(angle, true).ToString(CultureInfo.InvariantCulture) + Postfix;
	}
}