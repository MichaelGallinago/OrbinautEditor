using Godot;
using System;

public partial class BigTilePanel : Panel
{
	private CollisionEditorMain _screen;
	private CenterContainer _container;
	private BigTile _bigTile;
	private bool _isResetSize;

	public Vector2 PanelBorder { get; } = new(8, 8);

	public override void _Ready()
	{
		_screen = CollisionEditorMain.Screen;
		_bigTile = (BigTile)GetChild(0).GetChild(0);
		MinimumSizeChanged += () => _bigTile.CustomMinimumSize = CustomMinimumSize - PanelBorder;

		_container = (CenterContainer)GetParent();
		GetTree().Root.SizeChanged += ResetPanelSize;
		_screen.ActivityChangedEvents += UpdatePanelSize;
	}

	public override void _Process(double delta)
	{
		if (!_isResetSize) return;
		_isResetSize = false;
		UpdatePanelSize(true);
	}
	
	private void ResetPanelSize()
	{
		if (_bigTile.Texture is null) return;
		CustomMinimumSize = new Vector2();
		_isResetSize = true;
	}

	private void UpdatePanelSize(bool isActive)
	{
		if (!isActive)
		{
			_bigTile.Texture = null;
			CustomMinimumSize = new Vector2();
			return;
		}

		if (_bigTile.Texture is null) return;

		Vector2 textureSize = _bigTile.Texture.GetSize();
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
