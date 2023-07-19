using Godot;
using System;

public partial class BigTile : TextureRect
{
	private CollisionEditorMain _screen;
	private Control _canvas; 
	private Panel _panel;

	private readonly Vector2 _panelBorder = new(8, 8);
	public int TileScale { get; private set; }
	
	public override void _Ready()
	{
		_canvas = new BigTileCanvas(this);
		AddChild(_canvas);
		
		_panel = (Panel)GetParent();
		_screen = CollisionEditorMain.Screen;
		_screen.BigTile = this;
		
		_screen.ActivityChangedEvents += isActive => UpdateTile(isActive ? _screen.TileIndex : null);
		_screen.TileIndexChangedEvents += () => UpdateTile(_screen.TileIndex);
		_screen.AngleChangedEvents += _ => _canvas.QueueRedraw();
		
		var container = (BoxContainer)GetParent().GetParent();
		container.Resized += () =>
		{
			if (Texture is null) return;
			Vector2 textureSize = Texture.GetSize();
			TileScale = (int)(Mathf.Min(container.Size.X, container.Size.Y) 
			                  / Mathf.Max(textureSize.X, textureSize.Y));
			UpdateTile(_screen.TileIndex);
		};
	}

	public override void _Process(double delta)
	{
	}

	private void UpdateTile(int? tileIndex)
	{
		Texture = tileIndex is null ? new Texture2D() : _screen.TileSet.Tiles[(int)tileIndex].Sprite.Texture;
		CustomMinimumSize = Texture.GetSize() * TileScale;
		_panel.CustomMinimumSize = CustomMinimumSize + _panelBorder;
		_canvas.CustomMinimumSize = CustomMinimumSize;
		_canvas.QueueRedraw();
	}
}
