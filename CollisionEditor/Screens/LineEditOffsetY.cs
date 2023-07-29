using Godot;
using System;

public partial class LineEditOffsetY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Offset.Y = int.Parse(Text);
    }
}
