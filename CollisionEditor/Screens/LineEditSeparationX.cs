public partial class LineEditSeparationX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Parameters.Separation.X = int.Parse(Text);
    }
}
