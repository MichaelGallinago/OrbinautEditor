public partial class LineEditSeparationLoadX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        LoadTileMap.Parameters.Separation.X = int.Parse(Text);
    }
}
