namespace OrbinautEditor.SurfaceEditor.ViewModel;

public partial class LineEditSurfaceSizeY : General.ViewModel.LineEditValidableVector
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