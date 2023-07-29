public partial class LineEditTileSizeY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.TileSize.Y = int.Parse(Text);
    }
}
