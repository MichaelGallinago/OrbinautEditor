using Godot;

public partial class LineEditSeparationSaveY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMap.Parameters.Separation = new Vector2I(SaveTileMap.Parameters.Separation.X, int.Parse(Text));
    }
}
