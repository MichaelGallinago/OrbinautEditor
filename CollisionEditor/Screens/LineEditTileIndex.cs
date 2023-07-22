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
		_screen.TileIndexChangedEvents += UpdateMaxLength;
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
		UpdateMaxLength();
	}

	private void UpdateMaxLength()
	{
		MaxLength = _screen.TileSet.Tiles.Count.ToString().Length;
	}
}
