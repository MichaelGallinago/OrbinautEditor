using Godot;
using System;

public partial class LineEditSurfaceSizeY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out int value) && value >= 0;
    }

    protected override void OnTextValidated(string text)
    {
        SurfaceEditor.SurfaceSize.Y = int.Parse(Text);
    }
}
