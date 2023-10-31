namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class LineEditOffsetLoadY : General.ViewModel.LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        LoadTileMap.Parameters.Offset.Y = int.Parse(Text);
    }
}