using Godot;
using System;

public partial class BigTileCanvas : Control
{
	private CollisionEditorMain _screen;
	private readonly BigTile _bigTile;

	public BigTileCanvas(BigTile bigTile)
	{
		_bigTile = bigTile;
	}

	public override void _Ready()
	{
		_screen = CollisionEditorMain.Screen;
	}

	public override void _Draw()
	{
		if (_screen.AngleMap.Angles.Count == 0) return;
		Vector2 size = (Vector2)(_screen.TileSet.TileSize * _bigTile.TileScale) / 2f;
		Vector2 position = GetLinePosition(size);
		
		DrawLine(position + size, -position + size, Colors.Red, 2);
	}
	
	private Vector2 GetLinePosition(Vector2 size)
	{
		byte realAngle = _screen.AngleMap.Angles[_screen.TileIndex];
		float angle = Mathf.DegToRad((360 - (float)Angles.GetFullAngle(realAngle, false)) % 180);
		float invertedAngle = Mathf.Pi / 2 - angle;
		if (Mathf.Abs(invertedAngle) > Mathf.Atan2(size.Y, size.X))
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
