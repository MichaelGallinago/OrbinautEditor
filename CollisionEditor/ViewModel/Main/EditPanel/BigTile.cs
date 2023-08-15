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
		
		CollisionEditor.ActivityChangedEvents += isActive => UpdateTile(isActive ? CollisionEditor.TileIndex : null);
		CollisionEditor.TileIndexChangedEvents += () => UpdateTile(CollisionEditor.TileIndex);
		CollisionEditor.AngleChangedEvents += _ => OnAngleChanged();
	}

	public void UpdateTile(int? tileIndex)
	{
		if (CollisionEditor.TileSet.Tiles.Count == 0) return;
		Texture = tileIndex is null ? new Texture2D() : CollisionEditor.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
	}

	private void OnAngleChanged()
	{
		_canvasLine.QueueRedraw();
		_canvasLine.BorderCanvas.QueueRedraw();
		_canvasLine.BorderCanvasAlpha = byte.MaxValue;
	}
}
