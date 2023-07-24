using Godot;
using System;

public partial class BigTile : TextureRect
{
	public int TileScale { get; set; }
	private Control _canvas;
	
	private CollisionEditorMain _screen;

	public BigTile()
	{
		_canvas = new BigTileCanvas(this);
	}
	
	public override void _Ready()
	{
		AddChild(_canvas);

		_screen = CollisionEditorMain.Screen;
		_screen.BigTile = this;
		
		_screen.ActivityChangedEvents += isActive => UpdateTile(isActive ? _screen.TileIndex : null);
		_screen.TileIndexChangedEvents += () => UpdateTile(_screen.TileIndex);
		_screen.AngleChangedEvents += _ => _canvas.QueueRedraw();
	}

	public void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}
}
