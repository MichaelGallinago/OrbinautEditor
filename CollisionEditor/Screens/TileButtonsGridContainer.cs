using Godot;
using System;
using System.Collections.Generic;

public partial class TileButtonsGrid : GridContainer
{
	private readonly List<TextureButton> _tileButtons = new();
	private ButtonGroup _buttonGroup = new();

	public void CreateTileButtons(TileSet tileSet)
	{
		ClearTileButtons();
		
		Vector2I size = tileSet.TileSize * 2;
		foreach (Tile tile in tileSet.Tiles)
		{
			var textureButton = new TextureButton()
			{
				Size = size,
				ToggleMode = true,
				ButtonGroup = _buttonGroup,
				TextureNormal = tile.Sprite.Texture
			};
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
		TextureButton tileButton = CreateTextureButton(tileSet.TileSize, tileSet.Tiles[index]);
		_tileButtons.Insert(index, tileButton);
		AddChild(tileButton);
		MoveChild(tileButton, index);
	}

	private TextureButton CreateTextureButton(Vector2I size, Tile tile)
	{
		return new TextureButton()
		{
			Size = size,
			ToggleMode = true,
			ButtonGroup = _buttonGroup,
			TextureNormal = tile.Sprite.Texture
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
