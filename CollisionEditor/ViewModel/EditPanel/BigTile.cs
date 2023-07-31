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
		
		CollisionEditor.BigTile = this;
		
		CollisionEditor.ActivityChangedEvents += isActive => UpdateTile(isActive ? CollisionEditor.TileIndex : null);
		CollisionEditor.TileIndexChangedEvents += () => UpdateTile(CollisionEditor.TileIndex);
		CollisionEditor.AngleChangedEvents += _ => _canvasLine.QueueRedraw();
	}

	public void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : CollisionEditor.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}
}
