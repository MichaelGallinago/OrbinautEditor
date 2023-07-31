public partial class LineEditSeparationSaveX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        SaveTileMapScreen.Parameters.Separation.X = int.Parse(Text);
    }
}
