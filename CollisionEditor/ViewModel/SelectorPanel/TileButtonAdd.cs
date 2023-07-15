using Godot;
using System;

public partial class TileButtonAdd : Button
{
	private CollisionEditorMainScreen _screen;
	
	public override void _Ready()
	{
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive => Disabled = !isActive;
		
		Pressed += _screen.AddTile;
	}
}
