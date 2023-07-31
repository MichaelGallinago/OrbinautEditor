public partial class LineEditOffsetSaveY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMapScreen.Parameters.Offset.Y = int.Parse(Text);
    }
}