using Godot;
using System;

public partial class BigTile : TextureRect
{
	private CollisionEditorMainScreen _screen;
	private Panel _panel;
	
	private readonly Vector2 _panelBorder = new(8, 8);
	private const byte TileScale = 8;
	
	public override void _Ready()
	{
		_panel = (Panel)GetParent();
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.BigTile = this;

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
		CustomMinimumSize = Texture.GetSize() * TileScale;
		_panel.CustomMinimumSize = CustomMinimumSize + _panelBorder;
	}
}
