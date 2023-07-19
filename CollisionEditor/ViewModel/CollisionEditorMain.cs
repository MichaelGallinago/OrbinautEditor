using System;
using Godot;
using System.Collections.Generic;

public partial class CollisionEditorMain : Control
{
	public static CollisionEditorMain Screen { get; private set; }
	
	public TileSet TileSet { get; private set; }
	public AngleMap AngleMap { get; private set; }
	public TileButtonsGrid TileButtonsGrid { get; set; }
	public BigTile BigTile { get; set; }
	
	public int TileIndex 
	{
		get => _tileIndex;
		set
		{
			_tileIndex = Mathf.Wrap(value, 0, TileSet.Tiles.Count - 1);
			TileIndexChangedEvents?.Invoke();
		}
	}
	
	public event Action<bool> ActivityChangedEvents;
	public event Action TileIndexChangedEvents;
	public event Action<byte> AngleChangedEvents;

	private FileDialog _fileDialog;
	private FileDialog.FileSelectedEventHandler _fileDialogEvent;
	private readonly Vector2I _tileSize = new(16, 16);
	private int _tileCount;
	private int _tileIndex;

	public CollisionEditorMain()
	{
		Screen = this;
		TileSet = new TileSet(this);
		AngleMap = new AngleMap();
		_fileDialog = new FileDialog();
		TileIndexChangedEvents += () => AngleChangedEvents?.Invoke(AngleMap.Angles[TileIndex]);
	}

	public override void _Ready()
	{
		_fileDialog.Unresizable = true;
		_fileDialog.Size = GetTree().Root.Size;
		_fileDialog.Access = FileDialog.AccessEnum.Filesystem;
		_fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
		_fileDialog.InitialPosition = Window.WindowInitialPosition.CenterScreenWithKeyboardFocus;
		AddChild(_fileDialog);
	}

	public override void _Process(double delta)
	{
		UpdateTileIndex();

		ChangeActivity();
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
		TileIndex = TileIndex >= TileSet.Tiles.Count ? 0 : _tileIndex;
	}

	public void CreateAngleMap(string binaryFilePath)
	{
		AngleMap = new AngleMap(binaryFilePath, TileSet.Tiles.Count);
		
		if (TileSet.Tiles.Count == 0)
		{
			TileSet = new TileSet(this, AngleMap.Angles.Count, _tileSize);
			TileIndex = TileIndex >= TileSet.Tiles.Count ? 0 : _tileIndex;
		}
		TileButtonsGrid.CreateTileButtons(TileSet);
	}

	public void AddTile()
	{
		TileSet.InsertTile(TileIndex);
		AngleMap.InsertAngle(TileIndex);
		TileButtonsGrid.InsertTileButton(TileIndex, TileSet);
		TileIndexChangedEvents?.Invoke();
	}

	public void RemoveTile()
	{
		TileSet.RemoveTile(TileIndex);
		AngleMap.RemoveAngle(TileIndex);
		TileButtonsGrid.RemoveTileButton(TileIndex);
		TileIndexChangedEvents?.Invoke();
	}

	public void ChangeAngleBy(int value)
	{
		var angle = (byte)(AngleMap.Angles[TileIndex] + value);
		AngleMap.Angles[TileIndex] = angle;
		AngleChangedEvents?.Invoke(angle);
	}

	public void SetAngle(byte value)
	{
		AngleMap.Angles[TileIndex] = value;
		AngleChangedEvents?.Invoke(value);
	}

	private void UpdateTileIndex()
	{
		if (Input.IsActionJustPressed("Plus"))
		{
			TileIndex++;
		}
		else if (Input.IsActionJustPressed("Minus"))
		{
			TileIndex--;
		}
	}

	private void ChangeActivity()
	{
		if (_tileCount == TileSet.Tiles.Count || (_tileCount != 0 && TileSet.Tiles.Count != 0)) return;
		_tileCount = TileSet.Tiles.Count;
		ActivityChangedEvents?.Invoke(_tileCount != 0);
	}
}
