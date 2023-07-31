public partial class LineEditOffsetOpenX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        OpenTileMapScreen.Parameters.Offset.X = int.Parse(Text);
    }
}
