using System;
using Godot;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class CollisionEditor : Control
{
	private static readonly Vector2I BaseTileSize = new(16, 16);

	public static CollisionEditor Object { get; private set; }
	public static TileSet TileSet { get; private set; }
	public static AngleMap AngleMap { get; private set; }
	public static CustomFileDialog FileDialog { get; private set; }
	public static TileButtonsGrid TileButtonsGrid { get; set; }
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
	
	private static int _tileCount;
	private static int _tileIndex;

	public CollisionEditor()
	{
		Object = this;
		TileSet = new TileSet();
		AngleMap = new AngleMap();
		FileDialog = new CustomFileDialog();
		TileButtonsGrid = null;
		IsTileMode = false;

		_tileCount = 0;
		_tileIndex = 0;

		ActivityChangedEvents = null;
		TileIndexChangedEvents = null;
		AngleChangedEvents = null;

		TileIndexChangedEvents += () =>
		{
			if (AngleMap.Angles.Count == 0) return;
			AngleChangedEvents?.Invoke(AngleMap.Angles[TileIndex]);
		};
	}

	public override void _Ready()
	{
		Window window = GetTree().Root;
		
		FileDialog.Unresizable = true;
		FileDialog.Size = window.Size;
		FileDialog.Access = Godot.FileDialog.AccessEnum.Filesystem;
		FileDialog.FileMode = Godot.FileDialog.FileModeEnum.OpenFile;
		FileDialog.InitialPosition = Window.WindowInitialPosition.CenterScreenWithKeyboardFocus;
		AddChild(FileDialog);

		window.FilesDropped += OnFilesDropped;
	}

	public override void _Process(double delta)
	{
		ChangeActivity();
	}

	public async void CreateTileSet(string imagePath)
	{
		Image image = ImageFile.Open(imagePath);
		var packedScreen = GD.Load<PackedScene>("res://CollisionEditor/Screens/LoadTileMap.tscn");
		var screen = packedScreen.Instantiate<LoadTileMap>();
		LoadTileMap.Image = image;
		
		AddChild(screen);
		GetTree().Paused = true;
		LoadTileMapParameters parameters = await Task.Run(LoadTileMap.GetParameters);
		GetTree().Paused = false;
		screen.QueueFree();

		if (LoadTileMap.IsLoadPressed is null or false) return;

		TileSet = new TileSet(image, parameters.TileSize, 
			parameters.Separation, parameters.Offset, parameters.TileNumber);
		OnTileSetCreated();
	}
	
	public static void OpenTileSetFromCollisionMap(string filePath, 
		BinaryFile.Types fileType, bool isHeights)
	{
		if (!BinaryFile.TryOpen(filePath, fileType, out byte[] fileData)) return;
		CreateCollisionMap(fileData, isHeights);
	}

	public static void OpenAngleMap(string filePath)
	{
		if (!BinaryFile.TryOpen(filePath, BinaryFile.Types.Angles, out byte[] fileData)) return;
		CreateAngleMap(fileData);
	}

	private static void OnTileSetCreated()
	{
		AngleMap.SetAnglesCount(TileSet.Tiles.Count);
		TileButtonsGrid.CreateTileButtons(TileSet);
		TileIndex = TileIndex >= TileSet.Tiles.Count ? 0 : _tileIndex;
		_tileCount = 0;
	}

	public async Task<Image> CreateTileMap()
	{
		var packedScreen = GD.Load<PackedScene>("res://CollisionEditor/Screens/SaveTileMap.tscn");
		var screen = packedScreen.Instantiate<SaveTileMap>();

		AddChild(screen);
		GetTree().Paused = true;
		Image image = await Task.Run(SaveTileMap.GetImage);
		GetTree().Paused = false;
		screen.QueueFree();

		return SaveTileMap.IsSavePressed is null or false ? null : image;
	}

	public static void AddTile(int tileIndex)
	{
		TileSet.InsertTile(tileIndex);
		AngleMap.InsertAngle(tileIndex);
		TileButtonsGrid.InsertTileButton(tileIndex, TileSet);
		TileIndexChangedEvents?.Invoke();
	}

	public static void RemoveTile(int tileIndex)
	{
		TileSet.RemoveTile(tileIndex);
		AngleMap.RemoveAngle(tileIndex);
		TileButtonsGrid.RemoveTileButton(tileIndex);

		if (TileSet.Tiles.Count == 0) return;
		if (tileIndex >= TileSet.Tiles.Count)
		{
			TileIndex = TileSet.Tiles.Count - 1;
		}

		TileIndexChangedEvents?.Invoke();
	}

	public static void MoveTile(int fromTileIndex, int toTileIndex)
	{
		TileSet.MoveTile(fromTileIndex, toTileIndex);
		AngleMap.MoveAngle(fromTileIndex, toTileIndex);
		TileButtonsGrid.MoveTileButton(fromTileIndex, toTileIndex);
		TileIndex = toTileIndex;
		TileIndexChangedEvents?.Invoke();
	}

	public static void UpdateTile()
	{
		TileButtonsGrid.UpdateTileButton(TileIndex, TileSet);
		TileIndexChangedEvents?.Invoke();
	}

	public static void ClearAngles()
	{
		if (AngleMap.Angles.Count == 0) return;
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
		byte value = Angles.GetByteAngle(angle);
		AngleMap.Angles[tileIndex] = value;
		AngleChangedEvents?.Invoke(value);
	}
	
	private static void ChangeActivity()
	{
		if (_tileCount == TileSet.Tiles.Count || (_tileCount != 0 && TileSet.Tiles.Count != 0)) return;
		_tileCount = TileSet.Tiles.Count;

		ActivityChangedEvents?.Invoke(_tileCount != 0);
	}

	private static void CreateAngleMap(IEnumerable<byte> fileData)
	{
		AngleMap = new AngleMap(fileData, TileSet.Tiles.Count);

		if (TileSet.Tiles.Count != 0) return;
		TileSet = new TileSet(AngleMap.Angles.Count, BaseTileSize);
		OnTileSetCreated();
	}

	private static void CreateCollisionMap(IReadOnlyList<byte> fileData, bool isHeights)
	{
		TileSet = TileSet.CreateFromCollisions(fileData, isHeights);
		OnTileSetCreated();
	}

	private void OnFilesDropped(string[] paths)
	{
		string path = paths[0];
		string extension = '*' + Path.GetExtension(path);

		if (BinaryFile.Filters.All(filter => extension == filter.Key))
		{
			BinaryFile.Types type = BinaryFile.Open(path, out byte[] fileData);
			switch (type)
			{
				case BinaryFile.Types.Heights: 
					CreateCollisionMap(fileData, true);
					return;
				case BinaryFile.Types.Widths: 
					CreateCollisionMap(fileData, false);
					return;
				case BinaryFile.Types.Angles:
				default: 
					CreateAngleMap(fileData);
					return;
			}	
		}

		if (ImageFile.Filters.All(filter => extension != filter.Key)) return;
		CreateTileSet(path);
	}
}
