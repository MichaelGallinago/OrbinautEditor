public partial class LineEditOffsetSaveX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMapScreen.Parameters.Offset.X = int.Parse(Text);
    }
}
