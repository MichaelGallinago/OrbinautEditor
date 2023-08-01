using Godot;

public partial class LineEditSeparationSaveX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMap.Parameters.Separation = new Vector2I(int.Parse(Text), SaveTileMap.Parameters.Separation.Y);
    }
}
