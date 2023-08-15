using Godot;

public partial class BigTileCanvasLine : Control
{
	public Control BorderCanvas { get; }
	public double BorderCanvasAlpha { get; set; }

	private const float LineWidth = 2f;
	private const float AngleOffset = 0.01f;
	
	private BigTile _bigTile;
	private Vector2 _centre;
	private (Vector2, Vector2, Vector2) _positions;

	public BigTileCanvasLine(BigTile bigTile)
	{
		BorderCanvas = new Control();
		BorderCanvas.Draw += DrawBorder;
		_bigTile = bigTile;
		_bigTile.MinimumSizeChanged += () =>
		{
			CustomMinimumSize = _bigTile.CustomMinimumSize;
			BorderCanvas.CustomMinimumSize = CustomMinimumSize;
		};
	}

	public override void _Ready()
	{
		AddChild(BorderCanvas);
	}

	public override void _Process(double delta)
	{
		if (BorderCanvasAlpha <= 0d) return;
		BorderCanvasAlpha -= delta * byte.MaxValue;
		BorderCanvas.QueueRedraw();
	}

	public override void _Draw()
	{
		if (CollisionEditor.AngleMap.Angles.Count == 0) return;
		_centre = (Vector2)(CollisionEditor.TileSet.TileSize * _bigTile.TileScale) / 2f;
		_positions = GetLinePositions(_centre);
		
		DrawAngleLine(this, _positions.Item1, -_positions.Item1, Colors.Red);
	}

	private void DrawBorder()
	{
		Color color = Colors.Red;
		color.A8 = (byte)Mathf.Max(0, BorderCanvasAlpha);
		DrawAngleLine(BorderCanvas, -_positions.Item1, _positions.Item2, color);
		DrawAngleLine(BorderCanvas, _positions.Item2, _positions.Item3, color);
		DrawAngleLine(BorderCanvas, _positions.Item1, _positions.Item3, color);
	}

	private void DrawAngleLine(CanvasItem node, Vector2 from, Vector2 to, Color color)
	{
		node.DrawLine(from + _centre, to + _centre, color, LineWidth);
	}

	private static (Vector2, Vector2, Vector2) GetLinePositions(Vector2 centre)
	{
		byte byteAngle = CollisionEditor.AngleMap.Angles[CollisionEditor.TileIndex];
		float angle = 360f - (float)Angles.GetFullAngle(byteAngle, false);
		float fullAngle = Mathf.DegToRad(angle);
		float cornerAngle = Mathf.Atan2(centre.X, centre.Y);
		int direction = fullAngle + AngleOffset >= Mathf.Pi + cornerAngle || fullAngle < cornerAngle ? 1 : -1;

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
