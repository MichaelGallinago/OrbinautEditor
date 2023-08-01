using Godot;

public partial class LineEditOffsetSaveX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMap.Parameters.Offset = new Vector2I(int.Parse(Text), SaveTileMap.Parameters.Offset.Y);
    }
}
