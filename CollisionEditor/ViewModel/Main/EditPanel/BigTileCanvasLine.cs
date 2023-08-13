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
		Vector2 centre = (Vector2)(CollisionEditor.TileSet.TileSize * _bigTile.TileScale) / 2f;
		byte byteAngle = CollisionEditor.AngleMap.Angles[CollisionEditor.TileIndex];
		(Vector2, Vector2, Vector2) positions = GetLinePositions(centre, byteAngle);
		
		DrawLine(positions.Item1 + centre, -positions.Item1 + centre, Colors.Red, 2);
		DrawLine(-positions.Item1 + centre, positions.Item2 + centre, Colors.Red, 1);
		DrawLine(positions.Item2 + centre, positions.Item3 + centre, Colors.Red, 1);
		DrawLine(positions.Item1 + centre, positions.Item3 + centre, Colors.Red, 1);
	}
	
	private static (Vector2, Vector2, Vector2) GetLinePositions(Vector2 centre, byte byteAngle)
	{
		float fullAngle = 360f - (float)Angles.GetFullAngle(byteAngle, false);
		float origAngle = Mathf.DegToRad(fullAngle);
		float angle = Mathf.DegToRad(fullAngle % 180);
		float invertedAngle = Mathf.Pi / 2 - angle;

		Vector2 firstCorner, secondCorner;
		float cornerAngle = Mathf.Atan2(centre.X, centre.Y);
		int sign = origAngle + 0.01f >= Mathf.Pi + cornerAngle || origAngle < cornerAngle ? 1 : -1;
		if (Mathf.Abs(invertedAngle) > cornerAngle)
		{
			firstCorner = new Vector2(-centre.X, centre.Y * sign);
			secondCorner = new Vector2(centre.X, centre.Y * sign);
			centre.Y = Mathf.Tan(angle) * centre.X;
		}
		else
		{
			firstCorner = new Vector2(centre.X * sign, -centre.Y);
			secondCorner = new Vector2(centre.X * sign, centre.Y);
			centre.X = Mathf.Tan(invertedAngle) * centre.Y;
		}

		return (centre, firstCorner, secondCorner);
	}
}
