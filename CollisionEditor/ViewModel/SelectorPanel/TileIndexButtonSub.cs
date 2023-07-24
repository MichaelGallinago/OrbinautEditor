using Godot;
using System;

public partial class TileIndexButtonSub : Button
{
	public override void _Ready()
	{
		CollisionEditorMain screen = CollisionEditorMain.Screen;
		screen.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => screen.TileIndex--;
	}
}
