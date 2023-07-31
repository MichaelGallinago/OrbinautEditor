using Godot;
using System;

public partial class LineEditOffsetOpenY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        OpenTileMapScreen.Parameters.Offset.Y = int.Parse(Text);
    }
}
