public partial class LineEditTileSizeY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return byte.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        LoadTileMap.Parameters.TileSize.Y = int.Parse(Text);
    }
}
