public partial class LineEditSeparationX : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out uint value) && value <= short.MaxValue;
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Separation.X = int.Parse(Text);
    }
}
