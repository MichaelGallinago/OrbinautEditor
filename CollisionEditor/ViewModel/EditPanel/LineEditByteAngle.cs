public partial class LineEditByteAngle : LineEditValidableBase
{
	private const string BaseText = "0";

	public override void _Ready()
	{
		base._Ready();
		CollisionEditorMain.ActivityChangedEvents += OnActivityChanged;
		CollisionEditorMain.AngleChangedEvents += OnAngleChanged;
		TextValidated += OnTextValidated;
	}

	protected override bool ValidateText()
	{
		return byte.TryParse(Text, out _);
	}
	
	private void OnTextValidated(string text)
	{
		CollisionEditorMain.AngleChangedEvents -= OnAngleChanged;
		CollisionEditorMain.SetAngle(byte.Parse(text));
		CollisionEditorMain.AngleChangedEvents += OnAngleChanged;
	}

	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
	}

	private void OnAngleChanged(byte angle)
	{
		if (byte.TryParse(Text, out byte value) && value == CollisionEditorMain.AngleMap.Angles[CollisionEditorMain.TileIndex]) return;
		Text = angle.ToString();
	}
}
