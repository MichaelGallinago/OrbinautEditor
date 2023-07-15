#nullable enable
using System;
using Godot;
using System.Collections.Generic;

public partial class CollisionEditorMainScreen : Control
{
	public TileSet TileSet { get; private set; }
	public AngleMap AngleMap { get; private set; }
	public TileButtonsGrid TileButtonsGrid { get; set; }
	public BigTile BigTile { get; set; }
	public int TileIndex 
	{
		get => _tileIndex;
		set
		{
			_tileIndex = value;
			TileIndexChangedEvents();
		}
	}
	
	public event Action<bool> ActivityChangedEvents;
	public event Action TileIndexChangedEvents;

	private FileDialog _fileDialog;
	private FileDialog.FileSelectedEventHandler? _fileDialogEvent;
	private readonly Vector2I _tileSize = new(16, 16);
	private int _tileCount;
	private int _tileIndex;

	public CollisionEditorMainScreen()
	{
		TileSet = new TileSet(this);
		AngleMap = new AngleMap();
		_fileDialog = new FileDialog();
	}
	
	public override void _Ready()
	{
		Window window = GetTree().Root;
		_fileDialog.Unresizable = true;
		_fileDialog.Size = window.Size;
		_fileDialog.Access = FileDialog.AccessEnum.Filesystem;
		_fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
		_fileDialog.InitialPosition = Window.WindowInitialPosition.CenterScreenWithKeyboardFocus;
		AddChild(_fileDialog);
	}

	public override void _Process(double delta)
	{
		if (_tileCount == TileSet.Tiles.Count || (_tileCount != 0 && TileSet.Tiles.Count != 0)) return;
		_tileCount = TileSet.Tiles.Count;
		ActivityChangedEvents(_tileCount != 0);
	}

	public void OpenFileDialog(Dictionary<string, string> filters, 
		FileDialog.FileModeEnum fileMode, FileDialog.FileSelectedEventHandler newEvent)
	{
		_fileDialog.FileMode = fileMode;

		if (_fileDialogEvent is not null)
		{
			_fileDialog.FileSelected -= _fileDialogEvent;	
		}
		_fileDialogEvent = newEvent;
		_fileDialog.FileSelected += _fileDialogEvent;

		_fileDialog.ClearFilters();
		foreach (var filter in filters)
		{
			_fileDialog.AddFilter(filter.Key, filter.Value);	
		}
		_fileDialog.Show();
	}

	public void CreateTileSet(string imagePath)
	{
		TileSet = new TileSet(this, imagePath, _tileSize, 
			new Vector2I(), new Vector2I());
		AngleMap.SetAnglesCount(TileSet.Tiles.Count);
		TileButtonsGrid.CreateTileButtons(TileSet);
		SetTileIndex(TileIndex);
	}

	public void CreateAngleMap(string binaryFilePath)
	{
		AngleMap = new AngleMap(binaryFilePath, TileSet.Tiles.Count);
		
		if (TileSet.Tiles.Count == 0)
		{
			TileSet = new TileSet(this, AngleMap.Angles.Count, _tileSize);
			SetTileIndex(TileIndex);
		}
		TileButtonsGrid.CreateTileButtons(TileSet);
	}

	private void SetTileIndex(int value)
	{
		TileIndex = Mathf.Min(value, TileSet.Tiles.Count - 1);
	}
}
