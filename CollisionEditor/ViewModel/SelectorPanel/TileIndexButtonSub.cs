using Godot;

public partial class TileIndexButtonSub : Button
{
	public override void _Ready()
	{
		CollisionEditorMain.ActivityChangedEvents += isActive => Disabled = !isActive;
		Pressed += () => CollisionEditorMain.TileIndex--;
	}
}
