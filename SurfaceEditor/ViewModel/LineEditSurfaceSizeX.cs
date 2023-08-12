using Godot;
using System;

public partial class LineEditSurfaceSizeX : LineEditValidableVector
{    
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out int value) && value >= 0;
    }

    protected override void OnTextValidated(string text)
    {
        SurfaceEditor.SurfaceSize.X = int.Parse(Text);
    }
}
