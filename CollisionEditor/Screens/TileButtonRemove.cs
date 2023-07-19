using Godot;
using System;

public partial class TileButtonRemove : Button
{
	private CollisionEditorMain _screen;
	public override void _Ready()
	{
		_screen = CollisionEditorMain.Screen;
		_screen.ActivityChangedEvents += isActive => Disabled = !isActive;
		
		Pressed += _screen.RemoveTile;
	}
}
