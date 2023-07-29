public partial class LineEditTileSizeX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.TileSize.X = int.Parse(Text);
    }
}
