using Godot;
using System;

public partial class BigTile : TextureRect
{
	private CollisionEditorMain _screen;
	public Control Canvas { get; }

	public int TileScale { get; set; }

	public BigTile()
	{
		Canvas = new BigTileCanvas(this);
	}
	
	public override void _Ready()
	{
		AddChild(Canvas);
		
		_screen = CollisionEditorMain.Screen;
		_screen.BigTile = this;
		
		_screen.ActivityChangedEvents += isActive => UpdateTile(isActive ? _screen.TileIndex : null);
		_screen.TileIndexChangedEvents += () => UpdateTile(_screen.TileIndex);
		_screen.AngleChangedEvents += _ => Canvas.QueueRedraw();
	}

	public void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}
}
