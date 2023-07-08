#nullable enable
using Godot;
using System.Collections.Generic;

public partial class CollisionEditorMainScreen : Control
{
	private readonly Vector2I tileSize = new(16, 16);
	
	public TileSet TileSet { get; private set; }
	public AngleMap AngleMap { get; private set; }
	private FileDialog fileDialog;
	private FileDialog.FileSelectedEventHandler? fileDialogEvent;

	public CollisionEditorMainScreen()
	{
		TileSet = new TileSet(this);
		AngleMap = new AngleMap();
		fileDialog = new FileDialog();
	}
	
	public override void _Ready()
	{
		Window window = GetTree().Root;
		fileDialog.Unresizable = true;
		fileDialog.Size = window.Size;
		fileDialog.Access = FileDialog.AccessEnum.Filesystem;
		fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
		fileDialog.InitialPosition = Window.WindowInitialPosition.CenterScreenWithKeyboardFocus;
		AddChild(fileDialog);
	}

	public void OpenFileDialog(Dictionary<string, string> filters, 
		FileDialog.FileModeEnum fileMode, FileDialog.FileSelectedEventHandler newEvent)
	{
		fileDialog.FileMode = fileMode;

		if (fileDialogEvent is not null)
		{
			fileDialog.FileSelected -= fileDialogEvent;	
		}
		fileDialogEvent = newEvent;
		fileDialog.FileSelected += fileDialogEvent;

		fileDialog.ClearFilters();
		foreach (var filter in filters)
		{
			fileDialog.AddFilter(filter.Key, filter.Value);	
		}
		fileDialog.Show();
	}

	public void CreateTileSet(string imagePath)
	{
		TileSet = new TileSet(this, imagePath, tileSize, 
			new Vector2I(), new Vector2I());
		AngleMap.SetAnglesCount(TileSet.Tiles.Count);
	}

	public void CreateAngleMap(string binaryFilePath)
	{
		AngleMap = new AngleMap(binaryFilePath, TileSet.Tiles.Count);
		
		if (TileSet.Tiles.Count == 0)
		{
			TileSet = new TileSet(this, AngleMap.Angles.Count, tileSize);
		}
	}
}
