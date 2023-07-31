public partial class LineEditSeparationLoadY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        LoadTileMap.Parameters.Separation.Y = int.Parse(Text);
    }
}
