public partial class LineEditSeparationOpenY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        OpenTileMapScreen.Parameters.Separation.Y = int.Parse(Text);
    }
}
