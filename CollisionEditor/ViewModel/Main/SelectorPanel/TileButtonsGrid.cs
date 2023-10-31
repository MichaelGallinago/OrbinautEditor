using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using OrbinautEditor.CollisionEditor.Models;

namespace OrbinautEditor.CollisionEditor.ViewModel.Main.SelectorPanel;

public partial class TileButtonsGrid : GridContainer
{
	private const byte Separation = 4;
	private const byte ButtonScale = 2;
	private const double InputDelayChangeSpeed = 0.01d;
	
	private TileButton _hoveredTileButton;
	private TileButton _selectedTileButton;
	private readonly List<TileButton> _tileButtons = new();
	private PackedScene _packedButton;
	private Vector2I _buttonSize;
	private ImageTexture _frameTexture;
	private bool _isTileButtonFocused;
	private bool _isGrabFocus;
	private double _inputDelay;
	private double _inputTimer;
	
	private double InputDelay 
	{ 
		get => _inputDelay;
		set => _inputDelay = Mathf.Clamp(value, 0.04d, 0.25d);
	}

	public override void _Ready()
	{
		_packedButton = GD.Load<PackedScene>("res://PackedObjects/tile_button.tscn");
		
		CollisionEditor.TileButtonsGrid = this;
		CollisionEditor.TileIndexChangedEvents += () =>
		{
			if (_tileButtons.Count == 0) return;
			TileButton button = _tileButtons[CollisionEditor.TileIndex];
			button.ButtonPressed = true;
			if (!_isGrabFocus) return;
			button.GrabFocus();
			_isGrabFocus = false;
		};
		
		Resized += () =>
		{
			int buttonSpace = _buttonSize.X + Separation;
			Columns = ((Vector2I)((Control)GetParent()).Size).X / buttonSpace;
			Size = new Vector2(buttonSpace * Columns, Size.Y);
		};

		GetTree().Root.GuiFocusChanged += control => _isTileButtonFocused = control is TileButton;
	}

	public override void _Process(double delta)
	{
		InputSelectionUpdate(delta);
		SelectedTileButtonUpdate();
	}

	public void CreateTileButtons(Models.TileSet tileSet)
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
	
	public void InsertTileButton(int index, Models.TileSet tileSet)
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
	
	public void UpdateTileButton(int index, Models.TileSet tileSet)
	{
		_tileButtons[index].TextureNormal = tileSet.Tiles[index].Sprite.Texture;
	}

	public void UpdateTileButtons(Models.TileSet tileSet)
	{
		for (var i = 0; i < tileSet.Tiles.Count; i++)
		{
			UpdateTileButton(i, tileSet);
		}
	}
	
	public void MoveTileButton(int fromTileIndex, int toTileIndex)
	{
		TileButton tileButton = _tileButtons[fromTileIndex];
		_tileButtons.RemoveAt(fromTileIndex);
		_tileButtons.Insert(toTileIndex, tileButton);
		MoveChild(tileButton, toTileIndex);
	}

	private TileButton CreateTileButton(Tile tile)
	{
		var button = _packedButton.Instantiate<TileButton>();
		button.CustomMinimumSize = _buttonSize;
		button.TextureNormal = tile.Sprite.Texture;
		button.FocusMode = FocusModeEnum.Click;

		button.Pressed += () =>
		{
			CollisionEditor.TileIndex = _tileButtons.IndexOf(button);
			_selectedTileButton = button;
		};
		
		button.Draw += () =>
		{
			if (button.IsHovered())
			{
				_hoveredTileButton = button;
			}
		};
		
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

	private void InputSelectionUpdate(double delta)
	{
		_inputTimer -= delta;
		if (!_isTileButtonFocused) return;
		if (Input.IsActionPressed("left"))
		{
			ChangeTileIndexByInput(-1);
		}
		else if (Input.IsActionPressed("right"))
		{
			ChangeTileIndexByInput(1);
		}
		else if (Input.IsActionPressed("up"))
		{
			ChangeTileIndexByInput(-Columns);
		}
		else if (Input.IsActionPressed("down"))
		{
			ChangeTileIndexByInput(Columns);
		}
		else
		{
			InputDelay++;
			_inputTimer = 0d;
		}
	}

	private void ChangeTileIndexByInput(int value)
	{
		if (_inputTimer > 0d) return; 
		_isGrabFocus = true;
		
		CollisionEditor.TileIndex = WarpTileIndex(
			CollisionEditor.TileSet.Tiles.Count, CollisionEditor.TileIndex + value);
		
		InputDelay -= InputDelayChangeSpeed;
		_inputTimer = InputDelay;
	}

	private int WarpTileIndex(int tileCount, int targetValue)
	{
		if (targetValue >= tileCount)
		{
			targetValue %= Columns;
		}
		else if (targetValue < 0)
		{
			targetValue += Mathf.CeilToInt((double)tileCount / Columns) * Columns;
			if (targetValue >= tileCount)
			{
				targetValue -= Columns;
			}
		}

		return targetValue;
	}
	
	private void SelectedTileButtonUpdate()
	{
		if (Input.IsActionPressed("click_left"))
		{
			if (_selectedTileButton is null || CollisionEditor.TileIndex == 0) return;
			int hoveredButtonIndex = _tileButtons.IndexOf(_hoveredTileButton);
			if (_selectedTileButton == _hoveredTileButton || hoveredButtonIndex == 0) return;
			CollisionEditor.MoveTile(_tileButtons.IndexOf(_selectedTileButton), hoveredButtonIndex);
			_hoveredTileButton = _selectedTileButton;
		}
		else
		{
			if (_selectedTileButton is null) return;
			_selectedTileButton = null;
		}
	}
}