public partial class LineEditSeparationY : LineEditValidableVector
{
    protected override bool ValidateText()
    {
        return uint.TryParse(Text, out uint value) && value <= short.MaxValue;
    }

    protected override void OnTextValidated(string text)
    {
        Screen.Separation.Y = int.Parse(Text);
    }
}
