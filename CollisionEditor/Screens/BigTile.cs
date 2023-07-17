using Godot;
using System;
using static Godot.Mathf;

public partial class BigTile : TextureRect
{
	private CollisionEditorMainScreen _screen;
	private Panel _panel;

	private readonly Vector2 _panelBorder = new(8, 8);
	private const byte TileScale = 8;
	
	public override void _Ready()
	{
		_panel = (Panel)GetParent();
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.BigTile = this;

		_screen.AngleChangedEvents += _ => QueueRedraw();
		_screen.ActivityChangedEvents += isActive => UpdateTile(isActive ? _screen.TileIndex : null);
		_screen.TileIndexChangedEvents += () => UpdateTile(_screen.TileIndex);
	}

	public override void _Process(double delta)
	{
		
	}

	public override void _Draw()
	{
		base._Draw();

		if (_screen.AngleMap.Angles.Count == 0) return;
		Vector2 size = (Vector2)(_screen.TileSet.TileSize * TileScale) / 2f;
		Vector2 position = GetLinePosition(size);
		
		DrawLine(position + size, -position + size, Colors.Red, 2);
	}

	private Vector2 GetLinePosition(Vector2 size)
	{
		byte realAngle = _screen.AngleMap.Angles[_screen.TileIndex];
		float angle = DegToRad((360 - (float)Angles.GetFullAngle(realAngle, false)) % 180);
		float invertedAngle = Pi / 2 - angle;
		if (Abs(invertedAngle) > Atan2(size.Y, size.X))
		{
			size.Y = Tan(angle) * size.X;
		}
		else
		{
			size.X = Tan(invertedAngle) * size.Y;
		}

		return size;
	}

	private void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
		CustomMinimumSize = Texture.GetSize() * TileScale;
		_panel.CustomMinimumSize = CustomMinimumSize + _panelBorder;
	}
}
