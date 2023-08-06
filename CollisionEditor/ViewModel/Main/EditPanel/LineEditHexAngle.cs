using System.Globalization;

public partial class LineEditHexAngle : LineEditValidableBase
{
	private const string BaseText = "0x00";
	private const int BasePrefixIndex = 0;
	private const byte BaseLength = 2;
	private int _prefixLength = 2;

	public override void _Ready()
	{
		base._Ready();
		CollisionEditor.TileIndexChangedEvents += () => Editable = CollisionEditor.TileIndex != 0;
		CollisionEditor.ActivityChangedEvents += OnActivityChanged;
		CollisionEditor.AngleChangedEvents += OnAngleChanged;
		TextValidated += OnTextValidated;
	}

	protected override bool ValidateText()
	{
		var value = string.Empty;

		foreach (string prefix in Angles.HexPrefixes)
		{
			if (!Text.StartsWith(prefix)) continue;
			_prefixLength = prefix.Length;
			value = Text[_prefixLength..];
			break;
		}
		
		return byte.TryParse(value, NumberStyles.HexNumber, null, out _);
	}

	private void OnTextValidated(string text)
	{
		CollisionEditor.AngleChangedEvents -= OnAngleChanged;
		CollisionEditor.SetAngle(byte.Parse(text[_prefixLength..], NumberStyles.HexNumber, null));
		CollisionEditor.AngleChangedEvents += OnAngleChanged;
	}
	
	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
	}

	private void OnAngleChanged(byte angle)
	{
		if (Text.Length < 3) return;
		if (byte.TryParse(Text[_prefixLength..], NumberStyles.HexNumber, null, out byte value) 
		    && value == CollisionEditor.AngleMap.Angles[CollisionEditor.TileIndex]) return;
		
		Text = Angles.GetHexAngle(angle, BaseLength, BasePrefixIndex);
	}
}
