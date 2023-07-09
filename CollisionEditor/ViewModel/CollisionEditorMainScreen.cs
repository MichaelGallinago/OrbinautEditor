#nullable enable
using Godot;
using System.Collections.Generic;

public partial class CollisionEditorMainScreen : Control
{
	public TileSet TileSet { get; private set; }
	public AngleMap AngleMap { get; private set; }
	
	private FileDialog _fileDialog;
	private FileDialog.FileSelectedEventHandler? _fileDialogEvent;
	private readonly Vector2I _tileSize = new(16, 16);
	private TileButtonsGrid _tileButtonsGrid;

	public CollisionEditorMainScreen()
	{
		TileSet = new TileSet(this);
		AngleMap = new AngleMap();
		_fileDialog = new FileDialog();
	}
	
	public override void _Ready()
	{
		_tileButtonsGrid = (TileButtonsGrid)GetTree().Root.GetNode("Control/ScreenContainer/MainPanelMarginContainer/MainPanel/SelectorPanelMarginContainer/Background/SelectorPanel/ScrollContainer/TileButtonsGrid");
		Window window = GetTree().Root;
		_fileDialog.Unresizable = true;
		_fileDialog.Size = window.Size;
		_fileDialog.Access = FileDialog.AccessEnum.Filesystem;
		_fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
		_fileDialog.InitialPosition = Window.WindowInitialPosition.CenterScreenWithKeyboardFocus;
		AddChild(_fileDialog);
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
		_tileButtonsGrid.CreateTileButtons(TileSet);
	}

	public void CreateAngleMap(string binaryFilePath)
	{
		AngleMap = new AngleMap(binaryFilePath, TileSet.Tiles.Count);
		
		if (TileSet.Tiles.Count == 0)
		{
			TileSet = new TileSet(this, AngleMap.Angles.Count, _tileSize);
		}
		_tileButtonsGrid.CreateTileButtons(TileSet);
	}
}
