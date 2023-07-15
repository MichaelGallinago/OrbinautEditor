using Godot;
using System;

public partial class TextEditTileIndex : TextEditValidable
{
	private CollisionEditorMainScreen _screen;
	private const string BaseText = "0";
	
	public override void _Ready()
	{
		TextValidated += UpdateIndex;

		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.ActivityChangedEvents += isActive =>
		{
			Text = isActive ? BaseText : string.Empty;
			Editable = isActive;
		};
	}

	protected override bool ValidateText()
	{
		return int.TryParse(Text, out int value) && value < _screen.TileSet.Tiles.Count;
	}

	private void UpdateIndex()
	{
		_screen.TileIndex = int.Parse(Text);
	}
}
