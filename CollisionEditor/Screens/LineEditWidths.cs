using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class LineEditWidths : LineEdit
{
	private CollisionEditorMainScreen _screen;

	public override void _Ready()
	{
		_screen = (CollisionEditorMainScreen)GetTree().Root.GetChild(0);
		_screen.TileIndexChangedEvents += () => Text = CreateString(_screen.TileSet.Tiles[_screen.TileIndex].Widths);
	}

	private static string CreateString(IEnumerable<byte> values)
	{
		var stringBuilder = new StringBuilder();
		foreach (byte value in values)
		{
			stringBuilder.Append(' ');
			stringBuilder.Append((char)((value < 10 ? '0' : 'A' - 10) + value));
		}
		return stringBuilder.Append(' ').ToString();
	}
}
