public partial class LineEditTileIndex : LineEditValidableBase
{
	private const string BaseText = "0";
	private int _tileCount;

	private const float LetterWidth = 9f;
	private const int ButtonsOffset = 20;
	
	public override void _Ready()
	{
		base._Ready();
		TextValidated += OnTextValidated;
		CollisionEditor.ActivityChangedEvents += OnActivityChanged;
		CollisionEditor.TileIndexChangedEvents += OnTileIndexChanged;
		Resized += OnResized;
	}

	protected override bool ValidateText()
	{
		return uint.TryParse(Text, out uint value) && value < CollisionEditor.TileSet.Tiles.Count;
	}

	private void OnResized()
	{
		MaxLength = (int)((Size.X - ButtonsOffset) / LetterWidth);
		if (Text.Length <= MaxLength) return;
		Text = Text[..MaxLength];
	}

	private void OnTextValidated(string text)
	{
		CollisionEditor.TileIndex = int.Parse(text);
	}

	private void OnActivityChanged(bool isActive)
	{
		Text = isActive ? BaseText : string.Empty;
		Editable = isActive;
	}

	private void OnTileIndexChanged()
	{
		if (int.TryParse(Text, out int value) && value == CollisionEditor.TileIndex) return;
		Text = CollisionEditor.TileIndex.ToString();
	}
}
