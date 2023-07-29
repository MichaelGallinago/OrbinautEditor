public partial class LineEditSeparationY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return int.TryParse(Text, out _);
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Separation.Y = int.Parse(Text);
    }
}
