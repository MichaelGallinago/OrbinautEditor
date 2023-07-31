public partial class LineEditByteAngle : LineEditValidableBase
{
	private const string BaseText = "0";

	public override void _Ready()
	{
		base._Ready();
		CollisionEditor.ActivityChangedEvents += OnActivityChanged;
		CollisionEditor.AngleChangedEvents += OnAngleChanged;
		TextValidated += OnTextValidated;
	}

	protected override bool ValidateText()
	{
		return byte.TryParse(Text, out _);
	}
	
	private void OnTextValidated(string text)
	{
		CollisionEditor.AngleChangedEvents -= OnAngleChanged;
		CollisionEditor.SetAngle(byte.Parse(text));
		CollisionEditor.AngleChangedEvents += OnAngleChanged;
	}

	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
	}

	private void OnAngleChanged(byte angle)
	{
		if (byte.TryParse(Text, out byte value) && value == CollisionEditor.AngleMap.Angles[CollisionEditor.TileIndex]) return;
		Text = angle.ToString();
	}
}
