using Godot;
using System;

public partial class BigTile : Sprite2D
{
	private CollisionEditorMainScreen _screen;
	
	public override void _Ready()
	{
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.BigTile = this;
		Scale = new Vector2(8, 8);
		
		_screen.ActivityChangedEvents += isActive =>
			UpdateTile(isActive ? _screen.TileIndex : null);
		_screen.TileIndexChangedEvents += () =>
			UpdateTile(_screen.TileIndex);
	}

	public override void _Process(double delta)
	{
		
	}

	private void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}
}
