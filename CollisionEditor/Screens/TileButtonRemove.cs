using Godot;
using System;

public partial class TileButtonRemove : Button
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
		_screen.TileSet.RemoveTile(_screen.TileIndex);
		_screen.TileButtonsGrid.RemoveTileButton(_screen.TileIndex);
	}
}
