using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class LineEditHeights : LineEdit
{
	private CollisionEditorMain _screen;

	public override void _Ready()
	{
		CollisionEditorMain.TileIndexChangedEvents += () => Text = CreateString(CollisionEditorMain.TileSet.Tiles[CollisionEditorMain.TileIndex].Heights);
		CollisionEditorMain.ActivityChangedEvents += OnActivityChanged;
    }

    private void OnActivityChanged(bool isActive)
    {
        if (isActive) return;
        Text = string.Empty;
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
