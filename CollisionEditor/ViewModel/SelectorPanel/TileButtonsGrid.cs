using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TileButtonsGrid : GridContainer
{
	private readonly List<TileButton> _tileButtons = new();
	private PackedScene _packedButton;
	private Vector2I _buttonSize;
	private ImageTexture _frameTexture;
	
	private const byte Separation = 4;
	private const byte ButtonScale = 2;

	public override void _Ready()
	{
		_packedButton = GD.Load<PackedScene>("res://tile_button.tscn");
		
		CollisionEditor.TileButtonsGrid = this;
		CollisionEditor.TileIndexChangedEvents += () => _tileButtons[CollisionEditor.TileIndex].ButtonPressed = true;

		Resized += () =>
		{
			int buttonSpace = _buttonSize.X + Separation;
			Columns = ((Vector2I)((Control)GetParent()).Size).X / buttonSpace;
			Size = new Vector2(buttonSpace * Columns, Size.Y);
		};
	}

	public void CreateTileButtons(TileSet tileSet)
	{
		ClearTileButtons();
		
		_buttonSize = tileSet.TileSize * ButtonScale;
		foreach (TileButton tileButton in tileSet.Tiles.Select(CreateTileButton))
		{
			_tileButtons.Add(tileButton);
			AddChild(tileButton);
		}
	}

	public void RemoveTileButton(int index)
	{
		_tileButtons[index].Free();
		_tileButtons.RemoveAt(index);
	}
	
	public void InsertTileButton(int index, TileSet tileSet)
	{
		if (_buttonSize != tileSet.TileSize * ButtonScale)
		{
			throw new Exception("wrong tile size");
		}
		
		TileButton tileButton = CreateTileButton(tileSet.Tiles[index]);
		_tileButtons.Insert(index, tileButton);
		AddChild(tileButton);
		MoveChild(tileButton, index);
	}
	
	public void UpdateTileButton(int index, TileSet tileSet)
	{
		_tileButtons[index].TextureNormal = tileSet.Tiles[index].Sprite.Texture;
	}

	public void UpdateTileButtons(TileSet tileSet)
	{
		for (var i = 0; i < tileSet.Tiles.Count; i++)
		{
			UpdateTileButton(i, tileSet);
		}
	}

	private TileButton CreateTileButton(Tile tile)
	{
		var button = _packedButton.Instantiate<TileButton>();
		button.CustomMinimumSize = _buttonSize;
		button.TextureNormal = tile.Sprite.Texture;
		button.Pressed += () => CollisionEditor.TileIndex = _tileButtons.IndexOf(button);
		return button;
	}

	public void ClearTileButtons()
	{
		foreach (TileButton tileButton in _tileButtons)
		{
			tileButton.Free();
		}
		_tileButtons.Clear();
	}
}
