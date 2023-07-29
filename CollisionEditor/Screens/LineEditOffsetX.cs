using Godot;
using System;

public partial class LineEditOffsetX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out uint value) && value <= short.MaxValue;
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Offset.X = int.Parse(Text);
    }
}
