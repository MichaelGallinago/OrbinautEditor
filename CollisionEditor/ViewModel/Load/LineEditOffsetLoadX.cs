namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class LineEditOffsetLoadX : General.ViewModel.LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        LoadTileMap.Parameters.Offset.X = int.Parse(Text);
    }
}