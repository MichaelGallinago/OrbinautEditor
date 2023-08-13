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
		
		DrawLine(positions.Item1 + centre, -positions.Item1 + centre, Colors.Red, 2f);
		DrawLine(-positions.Item1 + centre, positions.Item2 + centre, Colors.Red, 2f);
		DrawLine(positions.Item2 + centre, positions.Item3 + centre, Colors.Red, 2f);
		DrawLine(positions.Item1 + centre, positions.Item3 + centre, Colors.Red, 2f);
	}
	
	private static (Vector2, Vector2, Vector2) GetLinePositions(Vector2 centre, byte byteAngle)
	{
		float angle = 360f - (float)Angles.GetFullAngle(byteAngle, false);
		float fullAngle = Mathf.DegToRad(angle);
		float cornerAngle = Mathf.Atan2(centre.X, centre.Y);
		int direction = fullAngle + 0.01f >= Mathf.Pi + cornerAngle || fullAngle < cornerAngle ? 1 : -1;

		return CalculatePositions(centre, direction, angle, cornerAngle);
	}

	private static (Vector2, Vector2, Vector2) CalculatePositions(
		Vector2 centre, int direction, float angle, float cornerAngle)
	{
		float halfAngle = Mathf.DegToRad(angle % 180);
		float invertedAngle = Mathf.Pi / 2f - halfAngle;
		Vector2 firstCorner, secondCorner;
		if (Mathf.Abs(invertedAngle) > cornerAngle)
		{
			firstCorner = new Vector2(-centre.X, centre.Y * direction);
			secondCorner = new Vector2(centre.X, centre.Y * direction);
			centre.Y = Mathf.Tan(halfAngle) * centre.X;
		}
		else
		{
			firstCorner = new Vector2(centre.X * direction, -centre.Y);
			secondCorner = new Vector2(centre.X * direction, centre.Y);
			centre.X = Mathf.Tan(invertedAngle) * centre.Y;
		}

		return (centre, firstCorner, secondCorner);
	}
}
