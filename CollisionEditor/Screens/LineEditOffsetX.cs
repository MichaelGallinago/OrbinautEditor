using Godot;
using System;

public partial class LineEditOffsetX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Parameters.Offset.X = int.Parse(Text);
    }
}
