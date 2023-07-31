public partial class LineEditSeparationSaveY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMapScreen.Parameters.Separation.Y = int.Parse(Text);
    }
}
