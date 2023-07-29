using Godot;
using System;

public partial class LineEditOffsetY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out uint value) && value <= short.MaxValue;
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Offset.Y = int.Parse(Text);
    }
}
