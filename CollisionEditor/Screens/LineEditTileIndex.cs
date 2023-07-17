using Godot;
using System;

public partial class LineEditTileIndex : LineEditValidable
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "0";
	
	public override void _Ready()
	{
		TextValidated += OnTextValidated;

		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive =>
		{
			Text = isActive ? BaseText : string.Empty;
			Editable = isActive;
			UpdateMaxLength();
		};
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

	private void UpdateMaxLength()
	{
		MaxLength = _screen.TileSet.Tiles.Count.ToString().Length;
	}
}
