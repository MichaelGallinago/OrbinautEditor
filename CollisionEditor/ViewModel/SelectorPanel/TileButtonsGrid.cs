using Godot;
using System;
using System.Collections.Generic;

public partial class TileButtonsGrid : GridContainer
{
	private readonly List<TextureButton> _tileButtons = new();
	private ButtonGroup _buttonGroup = new();
	private CollisionEditorMainScreen _screen;
	private Vector2I _buttonSize;
	
	private const byte Separation = 4;
	private const byte ButtonScale = 2;

	public override void _Ready()
	{
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.TileButtonsGrid = this;
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
		foreach (Tile tile in tileSet.Tiles)
		{
			TextureButton textureButton = CreateTileButton(tile);
			_tileButtons.Add(textureButton);
			AddChild(textureButton);
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
		
		TextureButton tileButton = CreateTileButton(tileSet.Tiles[index]);
		_tileButtons.Insert(index, tileButton);
		AddChild(tileButton);
		MoveChild(tileButton, index);
	}

	private TextureButton CreateTileButton(Tile tile)
	{
		return new TextureButton()
		{
			ToggleMode = true,
			ButtonGroup = _buttonGroup,
			CustomMinimumSize = _buttonSize,
			TextureNormal = tile.Sprite.Texture,
			StretchMode = TextureButton.StretchModeEnum.Scale
		};
	}

	private void ClearTileButtons()
	{
		foreach (TextureButton tileButton in _tileButtons)
		{
			tileButton.Free();
		}
		_tileButtons.Clear();
	}
}
