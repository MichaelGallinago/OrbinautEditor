using Godot;
using System;

public partial class LineEditOffsetY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Parameters.Offset.Y = int.Parse(Text);
    }
}
