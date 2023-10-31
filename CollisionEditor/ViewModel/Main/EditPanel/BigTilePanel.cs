using Godot;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.EditPanel;

public partial class BigTilePanel : Panel
{
	private CenterContainer _container;
	private BigTile _bigTile;
	private bool _isResetSize;

	private readonly Vector2 _panelBorder = new(8, 8);

	public override void _Ready()
	{
		_bigTile = (BigTile)GetChild(0).GetChild(0);
		MinimumSizeChanged += () => _bigTile.CustomMinimumSize = CustomMinimumSize - _panelBorder;

		_container = (CenterContainer)GetParent();
		GetTree().Root.SizeChanged += ResetPanelSize;
		CollisionEditor.ActivityChangedEvents += UpdatePanelSize;
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
		Vector2 targetSize = _container.Size - _panelBorder;
		float textureMaxSize = Mathf.Max(textureSize.X, textureSize.Y);
		float containerMinSize = Mathf.Min(targetSize.X, targetSize.Y);
		int scale = Mathf.Max(4, (int)(containerMinSize / textureMaxSize));
		
		_bigTile.TileScale = scale;
		_bigTile.UpdateTile(CollisionEditor.TileIndex);
		((ShaderMaterial)_bigTile.Material).SetShaderParameter("Size",  scale);
		CustomMinimumSize = textureSize * scale + _panelBorder;
	}
}