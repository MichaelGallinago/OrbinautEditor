using Godot;

public partial class BigTileCanvasLine : Control
{
	private readonly BigTile _bigTile;

	public BigTileCanvasLine(BigTile bigTile)
	{
		_bigTile = bigTile;
		_bigTile.MinimumSizeChanged += () => CustomMinimumSize = _bigTile.CustomMinimumSize;
	}

	public override void _Draw()
	{
		if (CollisionEditor.AngleMap.Angles.Count == 0) return;
		Vector2 size = (Vector2)(CollisionEditor.TileSet.TileSize * _bigTile.TileScale) / 2f;
		Vector2 position = GetLinePosition(size, CollisionEditor.AngleMap.Angles[CollisionEditor.TileIndex]);
		
		DrawLine(position + size, -position + size, Colors.Red, 2);
	}
	
	private static Vector2 GetLinePosition(Vector2 size, byte realAngle)
	{
		float angle = Mathf.DegToRad((360 - (float)Angles.GetFullAngle(realAngle, false)) % 180);
		float invertedAngle = Mathf.Pi / 2 - angle;
		
		if (Mathf.Abs(invertedAngle) > Mathf.Atan2(size.X, size.Y))
		{
			size.Y = Mathf.Tan(angle) * size.X;
		}
		else
		{
			size.X = Mathf.Tan(invertedAngle) * size.Y;
		}

		return size;
	}
}
