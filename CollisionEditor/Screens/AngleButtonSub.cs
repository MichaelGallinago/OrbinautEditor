using Godot;
using System;

public partial class AngleButtonSub : Button
{
	private CollisionEditorMainScreen _screen;
	public override void _Ready()
	{
		Pressed += OnPressed;
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive => Disabled = !isActive;
	}

	private void OnPressed()
	{
		_screen.AngleMap.Angles[_screen.TileIndex]--;
	}
}
