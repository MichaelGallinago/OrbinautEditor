namespace OrbinautEditor.CollisionEditor.ViewModel.Load;

public partial class LineEditTileSizeY : General.ViewModel.LineEditValidableVector
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