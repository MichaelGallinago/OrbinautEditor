public partial class LineEditSeparationOpenX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        OpenTileMapScreen.Parameters.Separation.X = int.Parse(Text);
    }
}
