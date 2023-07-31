public partial class LineEditTileSizeX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return byte.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        LoadTileMap.Parameters.TileSize.X = int.Parse(Text);
    }
}
