public partial class LineEditTileSizeX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return byte.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Parameters.TileSize.X = int.Parse(Text);
    }
}
