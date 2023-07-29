using Godot;
using System;

public partial class LineEditOffsetX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Offset.X = int.Parse(Text);
    }
}
