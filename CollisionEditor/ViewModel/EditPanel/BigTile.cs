using Godot;
using System;

public partial class BigTile : TextureRect
{
	public int TileScale { get; set; }
	private BigTileCanvasLine _canvasLine;
	private BigTileCanvasSquares _canvasSquares;
	
	private CollisionEditorMain _screen;

	public BigTile()
	{
		_canvasLine = new BigTileCanvasLine(this);
		_canvasSquares = new BigTileCanvasSquares(this);
	}
	
	public override void _Ready()
	{
		AddChild(_canvasSquares);
		AddChild(_canvasLine);

		_screen = CollisionEditorMain.Screen;
		_screen.BigTile = this;
		
		_screen.ActivityChangedEvents += isActive => UpdateTile(isActive ? _screen.TileIndex : null);
		_screen.TileIndexChangedEvents += () => UpdateTile(_screen.TileIndex);
		_screen.AngleChangedEvents += _ => _canvasLine.QueueRedraw();
	}

	public void UpdateTile(int? tileIndex)
	{
		if (_screen.TileSet.Tiles.Count == 0)
		{
			tileIndex = null;
		}
		
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}
}
