public partial class LineEditSeparationX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Separation.X = int.Parse(Text);
    }
}
