using Godot;
using System;

public partial class AngleButtonAdd : Button
{
	public override void _Ready()
	{
		CollisionEditorMain screen = CollisionEditorMain.Screen;
		screen.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => screen.ChangeAngleBy(1);
	}
}