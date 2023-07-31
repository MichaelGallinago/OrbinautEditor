using System;
using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class CollisionEditorMain : Control
{
	public static CollisionEditorMain Object { get; private set; }
	public static TileSet TileSet { get; private set; }
	public static AngleMap AngleMap { get; private set; }
	public static TileButtonsGrid TileButtonsGrid { get; set; }
	public static BigTile BigTile { get; set; }
	public static bool IsTileMode { get; set; }
	
	public static int TileIndex 
	{
		get => _tileIndex;
		set
		{
			_tileIndex = Mathf.Wrap(value, 0, TileSet.Tiles.Count);
			TileIndexChangedEvents?.Invoke();
		}
	}
	
	public static event Action<bool> ActivityChangedEvents;
	public static event Action TileIndexChangedEvents;
	public static event Action<byte> AngleChangedEvents;

	private static FileDialog _fileDialog;
	private static FileDialog.FileSelectedEventHandler _fileDialogEvent;
	private static readonly Vector2I TileSize = new(16, 16);
	private static int _tileCount;
	private static int _tileIndex;

	public CollisionEditorMain()
	{
		Object = this;
		TileSet = new TileSet(this);
		AngleMap = new AngleMap();
		_fileDialog = new FileDialog();
		
		TileIndexChangedEvents += () =>
		{
			if (AngleMap.Angles.Count == 0) return;
			AngleChangedEvents?.Invoke(AngleMap.Angles[TileIndex]);
		};
	}

	public  override void _Ready()
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
		ChangeActivity();
	}

	public static void OpenFileDialog(Dictionary<string, string> filters, 
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
		foreach (KeyValuePair<string, string> filter in filters)
		{
			_fileDialog.AddFilter(filter.Key, filter.Value);	
		}
		_fileDialog.Show();
	}

	public async void CreateTileSet(string imagePath)
	{
		Image image = ImageLoader.Load(imagePath);
		var packedScreen = GD.Load<PackedScene>("res://OpenTileMapScreen.tscn");
		var screen = packedScreen.Instantiate<OpenTileMapScreen>();
		OpenTileMapScreen.Image = image;
		
		AddChild(screen);
		GetTree().Paused = true;
		OpenTilemapParameters parameters = await Task.Run(OpenTileMapScreen.GetParameters);
		GetTree().Paused = false;
		RemoveChild(screen);

		TileSet = new TileSet(this, image, parameters.TileSize, 
			parameters.Separation, parameters.Offset, parameters.TileNumber);
		AngleMap.SetAnglesCount(TileSet.Tiles.Count);
		TileButtonsGrid.CreateTileButtons(TileSet);
		TileIndex = TileIndex >= TileSet.Tiles.Count ? 0 : _tileIndex;
	}

	public static void CreateAngleMap(string binaryFilePath)
	{
		AngleMap = new AngleMap(binaryFilePath, TileSet.Tiles.Count);
		
		if (TileSet.Tiles.Count == 0)
		{
			TileSet = new TileSet(Object, AngleMap.Angles.Count, TileSize);
			TileIndex = TileIndex >= TileSet.Tiles.Count ? 0 : _tileIndex;
		}
		TileButtonsGrid.CreateTileButtons(TileSet);
	}

	public async Task<Image> CreateTileMap()
	{
		var packedScreen = GD.Load<PackedScene>("res://SaveTileMapScreen.tscn");
		var screen = packedScreen.Instantiate<SaveTileMapScreen>();

		AddChild(screen);
		GetTree().Paused = true;
		SaveTilemapParameters parameters = await Task.Run(() => SaveTileMapScreen.GetParameters());
		GetTree().Paused = false;
		RemoveChild(screen);

		return await TileSet.CreateTileMap(parameters.Columns, new[] { Colors.Yellow, Colors.Black, Colors.White },
			parameters.GroupOffset, parameters.Separation, parameters.Offset);
	}

	public static void AddTile()
	{
		TileSet.InsertTile(TileIndex);
		AngleMap.InsertAngle(TileIndex);
		TileButtonsGrid.InsertTileButton(TileIndex, TileSet);
		TileIndexChangedEvents?.Invoke();
	}

	public static void RemoveTile()
	{
		TileSet.RemoveTile(TileIndex);
		AngleMap.RemoveAngle(TileIndex);
		TileButtonsGrid.RemoveTileButton(TileIndex);

		if (TileSet.Tiles.Count == 0) return;
		if (TileIndex >= TileSet.Tiles.Count)
		{
			TileIndex = TileSet.Tiles.Count - 1;
		}

		TileIndexChangedEvents?.Invoke();
	}

	public static void UpdateTile()
	{
		TileButtonsGrid.UpdateTileButton(TileIndex, TileSet);
		TileIndexChangedEvents?.Invoke();
	}

	public static void ClearAngles()
	{
		AngleMap.CreateAngles(AngleMap.Angles.Count);
		AngleChangedEvents?.Invoke(AngleMap.Angles[TileIndex]);
	}
	
	public static void ClearTiles()
	{
		TileSet.UnloadTiles();
		TileButtonsGrid.UpdateTileButtons(TileSet);
		TileIndexChangedEvents?.Invoke();
	}

	public static void ChangeAngleBy(int value)
	{
		var angle = (byte)(AngleMap.Angles[TileIndex] + value);
		AngleMap.Angles[TileIndex] = angle;
		AngleChangedEvents?.Invoke(angle);
	}

	public static void SetAngle(byte value)
	{
		AngleMap.Angles[TileIndex] = value;
		AngleChangedEvents?.Invoke(value);
	}
	
	public static void SetAngleFromLine(int tileIndex, Vector2I positionGreen, Vector2I positionBlue)
	{
		double angle = Math.Atan2(positionBlue.Y - positionGreen.Y, positionBlue.X - positionGreen.X);
		var value = (byte)(angle * AngleMap.ConvertRadiansToByte);
		AngleMap.Angles[tileIndex] = value;
		AngleChangedEvents?.Invoke(value);
	}


	private static void ChangeActivity()
	{
		if (_tileCount == TileSet.Tiles.Count || (_tileCount != 0 && TileSet.Tiles.Count != 0)) return;
		_tileCount = TileSet.Tiles.Count;

		ActivityChangedEvents?.Invoke(_tileCount != 0);
	}
}
