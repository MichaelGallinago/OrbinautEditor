public partial class LineEditTileSizeY : LineEditValidableVector
{
    private const int TileSizeLimit = 512; 
    
    protected override bool ValidateText()
    {
        return byte.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Parameters.TileSize.Y = int.Parse(Text);
    }
}
