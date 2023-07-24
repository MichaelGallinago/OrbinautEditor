using Godot;
using System;

public partial class LineEditTileIndex : LineEditValidable
{
	private CollisionEditorMain _screen;
	private const string BaseText = "0";
	
	public override void _Ready()
	{
		TextValidated += OnTextValidated;

		_screen = CollisionEditorMain.Screen;
		_screen.ActivityChangedEvents += OnActivityChanged;
		_screen.TileIndexChangedEvents += OnTileIndexChanged;
	}

	protected override bool ValidateText()
	{
		return int.TryParse(Text, out int value) && value < _screen.TileSet.Tiles.Count;
	}

	private void OnTextValidated(string text)
	{
		_screen.TileIndex = int.Parse(text);
	}

	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
		OnTileIndexChanged();
	}

	private void OnTileIndexChanged()
	{
		if (!int.TryParse(Text, out int value) || value != _screen.TileIndex)
		{
			Text = _screen.TileIndex.ToString();
		}
		
		MaxLength = _screen.TileSet.Tiles.Count.ToString().Length;
	}
}
