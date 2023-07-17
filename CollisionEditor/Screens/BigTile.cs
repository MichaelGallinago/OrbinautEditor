using Godot;
using System;

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
		float angle = (360 - (float)Angles.GetFullAngle(_screen.AngleMap.Angles[_screen.TileIndex], false)) % 180;

		Vector2 position;
		if (Mathf.Abs(90 - angle) > Mathf.RadToDeg(Mathf.Atan2(size.Y, size.X)))
		{
			float tangent = Mathf.Tan(Mathf.DegToRad(angle));
			position = new Vector2(size.X, tangent * size.X);
		}
		else
		{
			float tangent = -Mathf.Tan(Mathf.DegToRad(angle - 90));
			position = new Vector2(tangent * size.Y, size.Y);
		}
		DrawLine(position + size, -position + size, Colors.Red, 2);
	}

	private void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
		CustomMinimumSize = Texture.GetSize() * TileScale;
		_panel.CustomMinimumSize = CustomMinimumSize + _panelBorder;
	}
}
