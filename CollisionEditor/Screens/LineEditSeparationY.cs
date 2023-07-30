public partial class LineEditSeparationY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return ushort.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Parameters.Separation.Y = int.Parse(Text);
    }
}
