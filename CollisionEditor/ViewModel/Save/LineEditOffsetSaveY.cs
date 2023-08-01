using Godot;

public partial class LineEditOffsetSaveY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMap.Parameters.Offset = new Vector2I(SaveTileMap.Parameters.Offset.X, int.Parse(Text));
    }
}