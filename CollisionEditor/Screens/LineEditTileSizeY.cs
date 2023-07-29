public partial class LineEditTileSizeY : LineEditValidableVector
{
    private const int TileSizeLimit = 512; 
    
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out uint value) && value <= TileSizeLimit;
    }

    protected override void OnTextValidated(string text)
    {
        Screen.TileSize.Y = int.Parse(Text);
    }
}
