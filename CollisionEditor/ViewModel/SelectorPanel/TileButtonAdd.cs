using Godot;
using System;

public partial class TileButtonAdd : Button
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
		_screen.TileSet.InsertTile(_screen.TileIndex);
		_screen.TileButtonsGrid.InsertTileButton(_screen.TileIndex, _screen.TileSet);
	}
}
