using Godot;

public partial class BigTile : TextureRect
{
	public int TileScale { get; set; }
	private BigTileCanvasLine _canvasLine;
	private BigTileCanvasSquares _canvasSquares;

	public BigTile()
	{
		_canvasLine = new BigTileCanvasLine(this);
		_canvasSquares = new BigTileCanvasSquares(this);
	}
	
	public override void _Ready()
	{
		AddChild(_canvasSquares);
		AddChild(_canvasLine);
		
		CollisionEditorMain.BigTile = this;
		
		CollisionEditorMain.ActivityChangedEvents += isActive => UpdateTile(isActive ? CollisionEditorMain.TileIndex : null);
		CollisionEditorMain.TileIndexChangedEvents += () => UpdateTile(CollisionEditorMain.TileIndex);
		CollisionEditorMain.AngleChangedEvents += _ => _canvasLine.QueueRedraw();
	}

	public void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : CollisionEditorMain.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}
}
