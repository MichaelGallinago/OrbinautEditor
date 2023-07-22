using Godot;
using System;

public partial class BigTilePanel : Panel
{
	private CollisionEditorMain _screen;
	private CenterContainer _container;
	private BigTile _bigTile;

	public Vector2 PanelBorder { get; } = new(8, 8);

	public override void _Ready()
	{
		_screen = CollisionEditorMain.Screen;
		_bigTile = (BigTile)GetChild(0).GetChild(0);
		MinimumSizeChanged += () => _bigTile.CustomMinimumSize = CustomMinimumSize - PanelBorder;

		_container = (CenterContainer)GetParent();
		GetTree().Root.SizeChanged += ResetPanelSize;
		_container.Resized += UpdatePanelSize;
		_screen.ActivityChangedEvents += _ => UpdatePanelSize();
	}
	
	private void ResetPanelSize()
	{
		Texture2D texture = _bigTile.Texture;
		if (texture is null) return;
		
		CustomMinimumSize = new Vector2();
	}

	private void UpdatePanelSize()
	{
		Texture2D texture = _bigTile.Texture;
		if (texture is null) return;
		
		Vector2 textureSize = texture.GetSize();
		Vector2 targetSize = _container.Size - PanelBorder;
		float textureMaxSize = Mathf.Max(textureSize.X, textureSize.Y);
		float containerMinSize = Mathf.Min(targetSize.X, targetSize.Y);
		int scale = Mathf.Max(4, (int)(containerMinSize / textureMaxSize));
		
		_bigTile.TileScale = scale;
		_bigTile.UpdateTile(_screen.TileIndex);
		((ShaderMaterial)_bigTile.Material).SetShaderParameter("Size",  scale);
		CustomMinimumSize = textureSize * scale + PanelBorder;
	}
}
